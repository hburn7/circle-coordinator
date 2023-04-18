using Discord;

namespace circle_coordinator.Derivatives;

public static class CCEmbedBuilder
{
	public static EmbedBuilder FromError(string title, string message) => new EmbedBuilder()
	                                                                      .WithColor(Color.Red)
	                                                                      .WithTitle(title)
	                                                                      .WithDescription(message)
	                                                                      .AddCCDefaults();

	public static EmbedBuilder FromError(string message) => new EmbedBuilder()
	                                                        .WithColor(Color.Red)
	                                                        .WithDescription(message)
	                                                        .AddCCDefaults();

	public static EmbedBuilder FromWarning(string title, string message) => new EmbedBuilder()
	                                                                        .WithColor(Color.Orange)
	                                                                        .WithTitle(title)
	                                                                        .WithDescription(message)
	                                                                        .AddCCDefaults();

	public static EmbedBuilder FromWarning(string message) => new EmbedBuilder()
	                                                          .WithColor(Color.Orange)
	                                                          .WithDescription(message)
	                                                          .AddCCDefaults();

	public static EmbedBuilder FromSuccess(string title, string message) => new EmbedBuilder()
	                                                                        .WithColor(Color.Green)
	                                                                        .WithTitle(title)
	                                                                        .WithDescription(message)
	                                                                        .AddCCDefaults();

	public static EmbedBuilder FromSuccess(string message) => new EmbedBuilder()
	                                                          .WithColor(Color.Green)
	                                                          .WithDescription(message)
	                                                          .AddCCDefaults();

	public static EmbedBuilder FromInfo(string title, string message) => new EmbedBuilder()
	                                                                     .WithColor(Color.Blue)
	                                                                     .WithTitle(title)
	                                                                     .WithDescription(message)
	                                                                     .AddCCDefaults();

	public static EmbedBuilder FromInfo(string message) => new EmbedBuilder()
	                                                       .WithColor(Color.Blue)
	                                                       .WithDescription(message)
	                                                       .AddCCDefaults();

	private static EmbedBuilder AddCCDefaults(this EmbedBuilder builder) => builder.WithFooter("Circle Coordinator")
	                                                                               .WithCurrentTimestamp();
}