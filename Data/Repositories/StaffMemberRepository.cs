using circle_coordinator.Data.Context;
using circle_coordinator.Data.Repositories.Interfaces;
using circle_coordinator.Models.Entities;

namespace circle_coordinator.Data.Repositories;

public class StaffMemberRepository : RepositoryBase<StaffMember>, IStaffMemberRepository
{
	public StaffMemberRepository(CCDbContext context) : base(context) {}
}