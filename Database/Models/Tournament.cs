using circle_coordinator.Database.Models.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace circle_coordinator.Database.Models;

public class Tournament : BaseEntity
{
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int TournamentId { get; set; }
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
	///  A description of the tournament
	/// </summary>
	public string? Description { get; set; }
	/// <summary>
	///  The players in this tournament
	/// </summary>
	public ICollection<Player> Players { get; set; }
	/// <summary>
	///  The teams that are participating in this tournament
	/// </summary>
	public ICollection<Team> Teams { get; set; }
	/// <summary>
	///  The stages that are part of this tournament
	/// </summary>
	public ICollection<TournamentStage> Stages { get; set; }
}