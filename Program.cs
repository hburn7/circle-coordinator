using circle_coordinator.Data.Context;
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

	public static void Main(string[] args)
	{
		new Program().RunAsync().GetAwaiter().GetResult();
	}

	public async Task RunAsync()
	{
		var client = _services.GetRequiredService<DiscordShardedClient>();
		var interactionService = _services.GetRequiredService<InteractionService>();

		client.Log += LogAsync;
		interactionService.Log += LogAsync;

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

	private async Task LogAsync(LogMessage msg)
	{
		var severity = msg.Severity switch
		{
			LogSeverity.Critical => LogEventLevel.Fatal,
			LogSeverity.Error => LogEventLevel.Error,
			LogSeverity.Warning => LogEventLevel.Warning,
			LogSeverity.Info => LogEventLevel.Information,
			LogSeverity.Verbose => LogEventLevel.Verbose,
			LogSeverity.Debug => LogEventLevel.Debug,
			_ => LogEventLevel.Information
		};
		Log.Write(severity, msg.Exception, "[{Source}] {Message}", msg.Source, msg.Message);

		await Task.CompletedTask;
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