using circle_coordinator.Derivatives;
using Discord.Interactions;

namespace circle_coordinator.Base;

public class CCModuleBase<T> : InteractionModuleBase<T> where T : ShardedInteractionContext
{
	public async Task RespondErrorAsync(string message) => await RespondAsync(embeds: new[] { CCEmbedBuilder.FromError(message).Build() });
	public async Task RespondErrorAsync(string title, string message) => await RespondAsync(embeds: new[] { CCEmbedBuilder.FromError(title, message).Build() });
	public async Task RespondWarningAsync(string message) => await RespondAsync(embeds: new[] { CCEmbedBuilder.FromWarning(message).Build() });
	public async Task RespondWarningAsync(string title, string message) => await RespondAsync(embeds: new[] { CCEmbedBuilder.FromWarning(title, message).Build() });
	public async Task RespondSuccessAsync(string message) => await RespondAsync(embeds: new[] { CCEmbedBuilder.FromSuccess(message).Build() });
	public async Task RespondSuccessAsync(string title, string message) => await RespondAsync(embeds: new[] { CCEmbedBuilder.FromSuccess(title, message).Build() });
	public async Task RespondInfoAsync(string message) => await RespondAsync(embeds: new[] { CCEmbedBuilder.FromInfo(message).Build() });
	public async Task RespondInfoAsync(string title, string message) => await RespondAsync(embeds: new[] { CCEmbedBuilder.FromInfo(title, message).Build() });
	
	public async Task ReplyErrorAsync(string message) => await ReplyAsync(embed: CCEmbedBuilder.FromError(message).Build());
	public async Task ReplyErrorAsync(string title, string message) => await ReplyAsync(embed: CCEmbedBuilder.FromError(title, message).Build());
	public async Task ReplyWarningAsync(string message) => await ReplyAsync(embed: CCEmbedBuilder.FromWarning(message).Build());
	public async Task ReplyWarningAsync(string title, string message) => await ReplyAsync(embed: CCEmbedBuilder.FromWarning(title, message).Build());
	public async Task ReplySuccessAsync(string message) => await ReplyAsync(embed: CCEmbedBuilder.FromSuccess(message).Build());
	public async Task ReplySuccessAsync(string title, string message) => await ReplyAsync(embed: CCEmbedBuilder.FromSuccess(title, message).Build());
	public async Task ReplyInfoAsync(string message) => await ReplyAsync(embed: CCEmbedBuilder.FromInfo(message).Build());
	public async Task ReplyInfoAsync(string title, string message) => await ReplyAsync(embed: CCEmbedBuilder.FromInfo(title, message).Build());
}