using Discord.WebSocket;

namespace circle_coordinator.Handlers;

public class ButtonHandler
{
	private readonly DiscordShardedClient _client;

	public ButtonHandler(DiscordShardedClient client)
	{
		_client = client;

		_client.ButtonExecuted += HandleButton;
	}

	private async Task HandleButton(SocketMessageComponent component)
	{
		// todo: figure out how to handle the button click
		switch (component.Data.CustomId)
		{
			case "tournament-new-approve":
				await component.RespondAsync();
				break;
			case "tournament-new-cancel":
				await component.RespondAsync("Got it, no new tournament will be created.");
				break;
		}
	}
}