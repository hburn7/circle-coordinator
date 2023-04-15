using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace circle_coordinator;

public class Program
{
	public static Task Main(string[] args) => new Program().MainAsync(args);

	public async Task MainAsync(string[] args)
	{
		var appConfiguration = new ConfigurationBuilder()
		                       .SetBasePath(Directory.GetCurrentDirectory())
		                       .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false, true)
		                       .Build();

		string? pgsqlConnString = appConfiguration.GetConnectionString("DefaultConnection");
		if (string.IsNullOrWhiteSpace(pgsqlConnString))
		{
			throw new Exception("Connection string is null or empty. Please populate the connection string in appsettings.json");
		}

		CreateLogger(pgsqlConnString);

		using var host = Host.CreateDefaultBuilder(args)
		                     .ConfigureAppConfiguration((_, config) => { config.AddConfiguration(appConfiguration); })
		                     .ConfigureServices(s =>
		                     {
			                     s.AddSingleton<DiscordShardedClient>(x => new DiscordShardedClient(new DiscordSocketConfig
			                     {
				                     LogLevel = LogSeverity.Verbose,
				                     MessageCacheSize = 1000,
				                     AlwaysDownloadUsers = true,
				                     GatewayIntents = GatewayIntents.All
			                     }));
		                     })
		                     .UseSerilog()
		                     .Build();

		var client = host.Services.GetRequiredService<DiscordShardedClient>();

		client.Log += LogDiscordEvent;

		// Get appsettings.json value
		string? token = host.Services.GetRequiredService<IConfiguration>()["Discord:Token"];

		if (string.IsNullOrEmpty(token))
		{
			throw new Exception("Token is null or empty. Please populate the token in appsettings.json");
		}

		try
		{
			await client.LoginAsync(TokenType.Bot, token);

			await host.RunAsync();
		}
		catch (Exception e)
		{
			Log.Fatal(e, "Application encountered an unhandled exception: {Message}", e.Message);
			throw;
		}
	}

	private void CreateLogger(string connString) => Log.Logger = new LoggerConfiguration()
	                                                             .MinimumLevel.Verbose()
	                                                             .Enrich.FromLogContext()
	                                                             .WriteTo.Console(theme: AnsiConsoleTheme.Code)
	                                                             .WriteTo.File($"logs/{DateTime.Now:u}.txt")
	                                                             .WriteTo.PostgreSQL(connString, "Logs", restrictedToMinimumLevel: LogEventLevel.Verbose)
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
			case LogSeverity.Debug:
				Log.Debug(msg.Exception, msg.Message);
				break;
		}

		return Task.CompletedTask;
	}
}