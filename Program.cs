using circle_coordinator.Database;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace circle_coordinator;

public class Program
{
	private readonly IConfiguration _appConfiguration;
	private readonly DiscordSocketConfig _discordSocketConfig = new()
	{
		LogLevel = LogSeverity.Verbose,
		MessageCacheSize = 1000,
		AlwaysDownloadUsers = true,
		GatewayIntents = GatewayIntents.All,
		LogGatewayIntentWarnings = false,
		SuppressUnknownDispatchWarnings = true
	};
	private readonly IServiceProvider _services;

	public Program()
	{
		_appConfiguration = new ConfigurationBuilder()
		                    .SetBasePath(Directory.GetCurrentDirectory())
		                    .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false, true)
		                    .Build();

		string? pgsqlConnString = _appConfiguration.GetConnectionString("DefaultConnection");

		if (string.IsNullOrWhiteSpace(pgsqlConnString))
		{
			throw new Exception("Connection string is null or empty. Please populate the connection string in appsettings.json");
		}

		_services = new ServiceCollection()
		            .AddSingleton(_discordSocketConfig)
		            .AddSingleton(_appConfiguration)
		            .AddSingleton<DiscordShardedClient>()
		            .AddSingleton<InteractionService>(serviceProvider =>
		            {
			            var client = serviceProvider.GetRequiredService<DiscordShardedClient>();
			            return new InteractionService(client, new InteractionServiceConfig
			            {
				            LogLevel = LogSeverity.Verbose,
				            DefaultRunMode = RunMode.Async
			            });
		            })
		            .AddSingleton<InteractionHandler>()
		            .AddDbContext<CCDbContext>(x => x.UseNpgsql())
		            .BuildServiceProvider();

		CreateLogger(pgsqlConnString);
	}

	public static void Main(string[] args) => new Program().RunAsync().GetAwaiter().GetResult();

	public async Task RunAsync()
	{
		var client = _services.GetRequiredService<DiscordShardedClient>();
		var interactionService = _services.GetRequiredService<InteractionService>();

		client.Log += LogDiscordEvent;
		interactionService.Log += LogDiscordEvent;

		// == BEGIN APP LAUNCH ==
		try
		{
			await _services.GetRequiredService<InteractionHandler>()
			               .InitializeAsync();

			await client.LoginAsync(TokenType.Bot, _appConfiguration["Discord:Token"]);
			await client.StartAsync();

			await Task.Delay(Timeout.Infinite);
		}
		catch (Exception e)
		{
			Log.Fatal(e, "Application encountered an unhandled exception: {Message}", e.Message);
			throw;
		}

		// == NO CODE HERE ==
	}

	private void CreateLogger(string connString) => Log.Logger = new LoggerConfiguration()
	                                                             .MinimumLevel.Verbose()
	                                                             .Enrich.FromLogContext()
	                                                             .WriteTo.Console(theme: AnsiConsoleTheme.Code)
	                                                             .WriteTo.File($"logs/{DateTime.Now:u}.txt")
	                                                             .WriteTo.PostgreSQL(connString, "Logs",
		                                                             restrictedToMinimumLevel: LogEventLevel.Verbose,
		                                                             needAutoCreateTable: true)
	                                                             .CreateLogger();

	private Task LogDiscordEvent(LogMessage msg)
	{
		switch (msg.Severity)
		{
			case LogSeverity.Critical:
				Log.Fatal(msg.Exception, msg.Message);
				break;
			case LogSeverity.Error:
				Log.Error(msg.Exception, msg.Message);
				break;
			case LogSeverity.Warning:
				Log.Warning(msg.Exception, msg.Message);
				break;
			case LogSeverity.Info:
				Log.Information(msg.Exception, msg.Message);
				break;
			case LogSeverity.Verbose:
				Log.Verbose(msg.Exception, msg.Message);
				break;
			case LogSeverity.Debug:
				Log.Debug(msg.Exception, msg.Message);
				break;
		}

		return Task.CompletedTask;
	}

	public static bool IsDebug()
	{
#if DEBUG
		return true;
#else
		return false;
#endif
	}
}