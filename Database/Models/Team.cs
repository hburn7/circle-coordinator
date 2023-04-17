using circle_coordinator.Database.Models.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace circle_coordinator.Database.Models;

/// <summary>
///  Represents a team in a tournament
/// </summary>
public class Team : BaseEntity
{
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int TeamId { get; set; }
	/// <summary>
	///  The tournament ID this team belongs to
	/// </summary>
	[Required]
	public int TournamentId { get; set; }
	/// <summary>
	///  The name of the team
	/// </summary>
	[Required]
	public string Name { get; set; }
	/// <summary>
	///  The players on this team
	/// </summary>
	[Required]
	public ICollection<Player> Players { get; set; }
	[Required]
	public ICollection<TeamPlayer> TeamPlayers { get; set; }
	/// <summary>
	///  The tournament this team belongs to
	/// </summary>
	[Required]
	public Tournament Tournament { get; set; }
}