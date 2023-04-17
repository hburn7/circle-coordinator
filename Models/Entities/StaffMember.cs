using circle_coordinator.Data.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace circle_coordinator.Models.Entities;

public class StaffMember : EntityBase
{
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int OsuId { get; set; }
	
	[Required]
	public int TournamentId { get; set; }
	public Tournament Tournament { get; set; }

	[Required]
	public ulong DiscordUserId { get; set; }

	[Required]
	public string DiscordTag { get; set; }
	
	public string? Email { get; set; }

	public ICollection<StaffRole> StaffRoles { get; set; }
	public ICollection<StaffMemberRole> StaffMemberRoles { get; set; }
	public bool IsAdmin { get; set; }
}