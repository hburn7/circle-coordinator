using circle_coordinator.Data.Context;
using circle_coordinator.Handlers;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace circle_coordinator;

public class Program
{
	private static IConfiguration _appConfiguration;
	private static readonly DiscordSocketConfig _discordSocketConfig = new()
	{
		LogLevel = LogSeverity.Verbose,
		MessageCacheSize = 1000,
		AlwaysDownloadUsers = true,
		GatewayIntents = GatewayIntents.All,
		LogGatewayIntentWarnings = false,
		SuppressUnknownDispatchWarnings = true
	};
	private IHost _services;
	public Program() { CreateLogger(); }

	public static void Main(string[] args)
	{
		var program = new Program();
		program._services = CreateHostBuilder(args).Build();
		program._services.Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
	                                                                   .ConfigureServices(ConfigureServices);

	private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
	{
		_appConfiguration = hostContext.Configuration;

		string? pgsqlConnString = _appConfiguration.GetConnectionString("DefaultConnection");

		if (string.IsNullOrWhiteSpace(pgsqlConnString))
		{
			throw new Exception("Connection string is null or empty. Please populate the connection string in appsettings.json");
		}

		services.AddSingleton(_discordSocketConfig)
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
		        .AddDbContext<CCDbContext>(x => x.UseNpgsql(pgsqlConnString))
		        .Configure<HostOptions>(options => options.ShutdownTimeout = TimeSpan.FromSeconds(10))
		        .AddHostedService<Worker>();
	}

	private void CreateLogger() => Log.Logger = new LoggerConfiguration()
	                                            .MinimumLevel.Verbose()
	                                            .Enrich.FromLogContext()
	                                            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
	                                            .WriteTo.File($"logs/{DateTime.Now:u}.txt")
	                                            .WriteTo.PostgreSQL(_appConfiguration.GetConnectionString("DefaultConnection"), "Logs",
		                                            restrictedToMinimumLevel: LogEventLevel.Verbose,
		                                            needAutoCreateTable: true)
	                                            .CreateLogger();

	public static bool IsDebug()
	{
#if DEBUG
		return true;
#else
        return false;
#endif
	}
}