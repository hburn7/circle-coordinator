using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace circle_coordinator;

public class InteractionHandler
{
	private readonly DiscordShardedClient _client;
	private readonly IConfiguration _config;
	private readonly InteractionService _handler;
	private readonly IServiceProvider _services;

	public InteractionHandler(DiscordShardedClient client, InteractionService handler, IServiceProvider services, IConfiguration config)
	{
		_client = client;
		_handler = handler;
		_services = services;
		_config = config;
	}

	public async Task InitializeAsync()
	{
		_client.ShardReady += ReadyAsync;

		await _handler.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

		_client.InteractionCreated += HandleInteraction;
	}

	private async Task ReadyAsync(DiscordSocketClient arg)
	{
		if (Program.IsDebug())
		{
			await _handler.RegisterCommandsToGuildAsync(_config.GetValue<ulong>("DebugGuildId"));
		}
		else
		{
			await _handler.RegisterCommandsGloballyAsync();
		}
	}

	private async Task HandleInteraction(SocketInteraction interaction)
	{
		try
		{
			// Create an execution context that matches the generic type parameter of your InteractionModuleBase<T> modules.
			var context = new ShardedInteractionContext(_client, interaction);

			// Execute the incoming command.
			var result = await _handler.ExecuteCommandAsync(context, _services);

			if (!result.IsSuccess)
			{
				switch (result.Error)
				{
					case InteractionCommandError.UnmetPrecondition:
						// implement
						break;
				}
			}
		}
		catch
		{
			// If Slash Command execution fails it is most likely that the original interaction acknowledgement will persist. It is a good idea to delete the original
			// response, or at least let the user know that something went wrong during the command execution.
			if (interaction.Type is InteractionType.ApplicationCommand)
			{
				await interaction.GetOriginalResponseAsync().ContinueWith(async msg => await msg.Result.DeleteAsync());
			}
		}
	}
}