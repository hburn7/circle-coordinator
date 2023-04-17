using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace circle_coordinator.Models.Entities;

/// <summary>
///  Represents a replay file that belongs to a specific stage of a given tournament.
///  e.g. a replay file for HR3 in Grand Finals of some tournament
/// </summary>
public class Replay : EntityBase
{
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int ReplayId { get; set; }
	/// <summary>
	///  The absolute path to the replay file on the server.
	/// </summary>
	[Required]
	public string FilePath { get; set; }
	/// <summary>
	///  The name of the replay file.
	/// </summary>
	[Required]
	public string FileName { get; set; }
	/// <summary>
	///  The ID of the stage that this replay belongs to.
	/// </summary>
	[Required]
	public int StageId { get; set; }
	/// <summary>
	///  The stage that this replay belongs to.
	/// </summary>
	[Required]
	public TournamentStage Stage { get; set; }
}