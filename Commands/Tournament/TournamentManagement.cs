using Discord.Interactions;

namespace circle_coordinator.Commands.Tournament;

public class TournamentManagementCommands : InteractionModuleBase<ShardedInteractionContext>
{
	[SlashCommand("foo", "This is a test command")]
	public async Task Foo() => await RespondAsync("Hello!");
}