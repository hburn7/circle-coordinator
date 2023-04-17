using circle_coordinator.Handlers;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace circle_coordinator
{
	public class Worker : BackgroundService
	{
		private readonly IServiceProvider _services;
		public Worker(IServiceProvider services) { _services = services; }

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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

				await client.LoginAsync(TokenType.Bot, _services.GetService<IConfiguration>()?["Discord:Token"]);
				await client.StartAsync();

				await Task.Delay(Timeout.Infinite, stoppingToken);
			}
			catch (Exception e)
			{
				Log.Fatal(e, "Application encountered an unhandled exception: {Message}", e.Message);
				throw;
			}
			// == NO CODE HERE ==
		}

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
	}
}