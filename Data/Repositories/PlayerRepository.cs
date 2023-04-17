using circle_coordinator.Data.Context;
using circle_coordinator.Data.Repositories.Interfaces;
using circle_coordinator.Models.Entities;

namespace circle_coordinator.Data.Repositories;

public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
{
	public PlayerRepository(CCDbContext context) : base(context) {}
}