using circle_coordinator.Base;
using circle_coordinator.Data.Repositories.Interfaces;
using circle_coordinator.Models.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using Discord;
using Discord.Interactions;
using System.Globalization;
using System.Text;

namespace circle_coordinator.Commands.Tournament;

[Group("tournament", "Commands for managing a tournament")]
[EnabledInDm(false)]
[DefaultMemberPermissions(GuildPermission.ManageGuild)]
public class TournamentManagementCommands : CCModuleBase<ShardedInteractionContext>
{
	private readonly ITournamentRepository _tournamentRepository;
	private readonly IStaffMemberRepository _staffMemberRepository;
	private readonly IStaffRoleRepository _staffRoleRepository;

	public TournamentManagementCommands(ITournamentRepository tournamentRepository, IStaffMemberRepository staffMemberRepository,
		IStaffRoleRepository staffRoleRepository)
	{
		_tournamentRepository = tournamentRepository;
		_staffMemberRepository = staffMemberRepository;
		_staffRoleRepository = staffRoleRepository;
	}

	[SlashCommand("new", "Creates a new tournament tied to this Discord server.")]
	public async Task CreateTournamentAsync(string name, string abbreviation, string? description = null,
		[Summary(description: "Leave blank to indicate no minimum")] int? minimumRank = null, 
		[Summary(description: "Leave blank to indicate no maximum.")] int? maximumRank = null,
		string? discordInviteUrl = null,
		string? forumPostUrl = null, string? bracketUrl = null,
		string? twitchUrl = null, string? youtubeUrl = null, string? twitterUrl = null)
	{
		var tournaments = _tournamentRepository.GetAllForGuild(Context.Guild.Id);
		if (tournaments.Any())
		{
			// respond with button
			var builder = new ComponentBuilder()
			              .WithButton("Yes", "tournament-new-approve", ButtonStyle.Success)
			              .WithButton("Cancel", "tournament-new-cancel", ButtonStyle.Danger)
			              .Build();

			await RespondAsync("There is already at least one tournament tied to this server, " +
			                   "are you sure you want to make another?", components: builder);
			
			// TODO: Figure out how to handle the button click
		}
		
		var tournament = new Models.Entities.Tournament
		{
			GuildId = Context.Guild.Id,
			Name = name,
			CreatorTag = Context.User.ToString(),
			CreatorId = Context.User.Id,
			Abbreviation = abbreviation,
			Description = description,
			DiscordPermanentInviteUrl = discordInviteUrl,
			ForumUrl = forumPostUrl,
			BracketUrl = bracketUrl,
			TwitchUrl = new List<string> { twitchUrl },
			YoutubeUrl = new List<string> { youtubeUrl },
			TwitterUrl = new List<string> { twitterUrl },
			MinimumRank = minimumRank,
			MaximumRank = maximumRank
		};

		await _tournamentRepository.AddAsync(tournament);

		await RespondAsync("Nice! You added a tournament!");
	}

	[SlashCommand("importstaff", "Adds staff to the tournament's database, including their roles.")]
	public async Task ImportStaffAsync(IAttachment csvFile)
	{
		/*
		 * StaffMember CSV Columns:
		   =======================
		   OsuId (int)
		   DiscordUserId (ulong)
		   DiscordTag (string)
		   Email (string)
		   Roles (string)
		   IsAdmin (bool)
		   
		   StaffRole CSV Columns:
		   =====================
		   StaffRoleId (int)
		   Name (string)
		 */
		
		var tournamentsForGuild = _tournamentRepository.GetAllForGuild(Context.Guild.Id);
		if (!tournamentsForGuild.Any())
		{
			await RespondErrorAsync("There are no tournaments for this server. Please create one first.");
			return;
		}
		
		if (!csvFile.Filename.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
		{
			await ReplyAsync("Please provide a valid CSV file.");
			return;
		}

		var staffMembers = new List<StaffMember>();

		using (var httpClient = new HttpClient())
		{
			using (var httpResponse = await httpClient.GetAsync(csvFile.Url))
			{
				httpResponse.EnsureSuccessStatusCode();
				using (var contentStream = await httpResponse.Content.ReadAsStreamAsync())
				using (var reader = new StreamReader(contentStream, Encoding.Default))
				{
					string csvContent = await reader.ReadToEndAsync();
					staffMembers = ParseCsv(csvContent);
				}
			}
		}

		await _staffMemberRepository.AddRangeAsync(staffMembers);

		var roleSb = new StringBuilder();
		// Get unique staff roles from all staff members and print
		var staffRoles = staffMembers.SelectMany(sm => sm.StaffRoles).Distinct().ToList();
		foreach (var role in staffRoles.DistinctBy(x => x.Name))
		{
			roleSb.AppendLine($"- {role.Name}");
		}

		var embed = new EmbedBuilder()
		            .WithTitle("Staff Members Imported Successfully")
		            .WithColor(Color.Green)
		            .WithDescription($"__**Staff:**__ {staffMembers.Count}\n__**Roles:**__ {roleSb}");

		await RespondAsync(embeds: new[] { embed.Build() });
	}
	
	private List<StaffMember> ParseCsv(string csvContent)
	{
		var staffMembers = new List<StaffMember>();
		var config = new CsvConfiguration(CultureInfo.InvariantCulture)
		{
			HasHeaderRecord = true,
			Delimiter = ","
		};

		using (var stringReader = new StringReader(csvContent))
		using (var csv = new CsvReader(stringReader, config))
		{
			csv.ReadHeader();
			while (csv.Read())
			{
				var staffMember = new StaffMember
				{
					OsuId = csv.GetField<int>("OsuId"),
					TournamentId = csv.GetField<int>("TournamentId"),
					DiscordUserId = ulong.Parse(csv.GetField<string>("DiscordUserId") ?? "0"),
					DiscordTag = csv.GetField<string>("DiscordTag") ?? string.Empty,
					Email = csv.GetField<string>("Email"),
					IsAdmin = csv.GetField<bool>("IsAdmin")
				};

				string[]? rolesStrRepr = csv.GetField<string>("Roles")?.Split(',');
				if (rolesStrRepr != null)
				{
					foreach (var rStr in rolesStrRepr)
					{
						var role = new StaffRole
						{
							Name = rStr
						};
						
						if (!staffMember.StaffRoles.Contains(role))
						{
							staffMember.StaffRoles.Add(role);
						}
					}
				}
				
				staffMembers.Add(staffMember);
			}
		}

		return staffMembers;
	}
}