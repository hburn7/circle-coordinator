using Discord.Interactions;

namespace circle_coordinator.Commands.Tournament;

[Group("tournament", "Commands for managing a tournament")]
public class TournamentManagementCommands : InteractionModuleBase<ShardedInteractionContext>
{
	[SlashCommand("create", "Creates a new tournament tied to this Discord server.")]
	public async Task CreateTournamentAsync()
	{
		await RespondAsync("You are so hot!");
	}
}