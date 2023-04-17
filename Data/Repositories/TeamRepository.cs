using circle_coordinator.Data.Context;
using circle_coordinator.Data.Repositories.Interfaces;
using circle_coordinator.Models.Entities;

namespace circle_coordinator.Data.Repositories;

public class TeamRepository : RepositoryBase<Team>, ITeamRepository
{
	public TeamRepository(CCDbContext context) : base(context) {}
}