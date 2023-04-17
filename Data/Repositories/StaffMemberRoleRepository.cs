using circle_coordinator.Data.Context;
using circle_coordinator.Data.Repositories.Interfaces;
using circle_coordinator.Models.Entities;

namespace circle_coordinator.Data.Repositories;

public class StaffMemberRoleRepository : RepositoryBase<StaffMemberRole>, IStaffMemberRoleRepository
{
	public StaffMemberRoleRepository(CCDbContext context) : base(context) {}
}