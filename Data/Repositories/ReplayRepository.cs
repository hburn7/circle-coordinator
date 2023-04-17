using circle_coordinator.Data.Context;
using circle_coordinator.Data.Repositories.Interfaces;
using circle_coordinator.Models.Entities;

namespace circle_coordinator.Data.Repositories;

public class ReplayRepository : RepositoryBase<Replay>, IReplayRepository
{
	public ReplayRepository(CCDbContext context) : base(context) {}
}