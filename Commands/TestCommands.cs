using Discord.Interactions;

namespace circle_coordinator.Commands;

public class TestCommands : InteractionModuleBase<ShardedInteractionContext>
{
	[SlashCommand("foo", "This is a test command")]
	public async Task Foo() => await RespondAsync("Hello!");

	[SlashCommand("bar", "This is a test command")]
	public async Task Bar() => await RespondAsync("Hello!");
}