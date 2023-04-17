using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace circle_coordinator.Models.Entities;

public class StaffRole : EntityBase
{
	[Required]
	public int TournamentId { get; set; }
	[Required]
	public Tournament Tournament { get; set; }
	[Required]
	public string Name { get; set; }

	public ulong? DiscordRoleId { get; set; }
	public string? Description { get; set; }
	
	public ICollection<StaffMember> StaffMembers { get; set; }
	public ICollection<StaffMemberRole> StaffMemberRoles { get; set; }
}