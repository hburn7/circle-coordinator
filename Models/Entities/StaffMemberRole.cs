using System.ComponentModel.DataAnnotations;

namespace circle_coordinator.Models.Entities;

public class StaffMemberRole : EntityBase
{
	[Required]
	public int StaffMemberId { get; set; }
	[Required]
	public StaffMember StaffMember { get; set; }
	[Required]
	public int StaffRoleId { get; set; }
	[Required]
	public StaffRole StaffRole { get; set; }
}