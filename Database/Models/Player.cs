using circle_coordinator.Database.Models.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace circle_coordinator.Database.Models;

/// <summary>
///  Represents a regular player
/// </summary>
public class Player : BaseEntity
{
	/// <summary>
	///  The player's osu! user id
	/// </summary>
	[Required]
	public int UserId { get; set; }
	public string? Username { get; set; }
	public string? DiscordTag { get; set; }
	public ulong? DiscordId { get; set; }
	public ICollection<Team> Teams { get; set; }
	public ICollection<TeamPlayer> TeamPlayers { get; set; }
	/// <summary>
	///  The tournament this player belongs to
	/// </summary>
	[Required]
	public ICollection<Tournament> Tournaments { get; set; }
}