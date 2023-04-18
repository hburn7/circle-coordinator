using circle_coordinator.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace circle_coordinator.Models.Entities;

public class Tournament : EntityBase
{
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int TournamentId { get; set; }
	/// <summary>
	/// The Id of the guild this was created in.
	/// </summary>
	[Required]
	public ulong GuildId { get; set; }
	/// <summary>
	/// The Id of the discord user who created this tournament
	/// </summary>
	[Required]
	public ulong CreatorId { get; set; }
	/// <summary>
	/// The full discord tag of the creator of this tournament
	/// </summary>
	[Required]
	public string CreatorTag { get; set; }
	/// <summary>
	///  The name of this tournament
	/// </summary>
	[Required]
	public string Name { get; set; }
	/// <summary>
	///  The tournament's abbreviation
	/// </summary>
	[Required]
	public string Abbreviation { get; set; }
	/// <summary>
	/// Range from 1 to INT32_MAX
	/// </summary>
	[Required]
	public int PlayersPerTeam { get; set; }
	/// <summary>
	/// The ruleset this tournament is using
	/// </summary>
	[Required]
	public Ruleset Ruleset { get; set; }
	/// <summary>
	///  A description of the tournament
	/// </summary>
	public string? Description { get; set; }
	/// <summary>
	/// The forum post url for this tournament
	/// </summary>
	public string? ForumUrl { get; set; }
	
	public int? MinimumRank { get; set; }
	public int? MaximumRank { get; set; }
	
	public string? DiscordPermanentInviteUrl { get; set; }
	public string? BracketUrl { get; set; }
	public List<string?> TwitterUrl { get; set; }
	public List<string?> TwitchUrl { get; set; }
	public List<string?> YoutubeUrl { get; set; }
	public List<string?> DonationUrls { get; set; }
	public DateTime? RegistrationOpenDate { get; set; }
	public DateTime? RegistrationCloseDate { get; set; }
	public DateTime? TournamentStartDate { get; set; }
	public DateTime? TournamentEndDate { get; set; }
	
	/// <summary>
	///  The players in this tournament
	/// </summary>
	public ICollection<Player> Players { get; set; }
	/// <summary>
	/// The staff in this tournament
	/// </summary>
	public ICollection<StaffMember> StaffMembers { get; set; }
	/// <summary>
	///  The teams that are participating in this tournament
	/// </summary>
	public ICollection<Team> Teams { get; set; }
	/// <summary>
	///  The stages that are part of this tournament
	/// </summary>
	public ICollection<TournamentStage> Stages { get; set; }
}