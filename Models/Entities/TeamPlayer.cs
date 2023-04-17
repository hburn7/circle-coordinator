using System.ComponentModel.DataAnnotations;

namespace circle_coordinator.Models.Entities;

/// <summary>
///  Represents a player on a team
/// </summary>
public class TeamPlayer : EntityBase
{
	/// <summary>
	///  The ID of the player this team player represents
	/// </summary>
	[Required]
	public int PlayerId { get; set; }
	/// <summary>
	///  The player this team player represents
	/// </summary>
	[Required]
	public Player Player { get; set; }
	/// <summary>
	///  The ID of the team this player belongs to
	/// </summary>
	[Required]
	public int TeamId { get; set; }
	/// <summary>
	///  The team this player belongs to
	/// </summary>
	[Required]
	public Team Team { get; set; }
}