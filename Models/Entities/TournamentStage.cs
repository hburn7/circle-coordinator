using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace circle_coordinator.Models.Entities;

/// <summary>
///  Represents a stage in a tournament, e.g. Ro32, Ro16, Semifinals, Finals, Grand Finals, etc.
/// </summary>
public class TournamentStage : EntityBase
{
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int StageId { get; set; }
	[Required]
	[MaxLength(100)]
	public string Name { get; set; }
	[MaxLength(10)]
	public string? Abbreviation { get; set; }
	[Required]
	public int TournamentId { get; set; }
	[Required]
	public Tournament Tournament { get; set; }
	public ICollection<Replay> Replays { get; set; }
}