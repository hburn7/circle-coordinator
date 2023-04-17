using circle_coordinator.Data.Context;
using circle_coordinator.Data.Repositories.Interfaces;
using circle_coordinator.Models.Entities;

namespace circle_coordinator.Data.Repositories;

public class StaffRoleRepository : RepositoryBase<StaffRole>, IStaffRoleRepository
{
	public StaffRoleRepository(CCDbContext context) : base(context) {}
}