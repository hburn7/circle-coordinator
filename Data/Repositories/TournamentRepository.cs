using circle_coordinator.Data.Context;
using circle_coordinator.Data.Repositories.Interfaces;
using circle_coordinator.Models.Entities;

namespace circle_coordinator.Data.Repositories;

public class TournamentRepository : RepositoryBase<Tournament>, ITournamentRepository
{
	public TournamentRepository(CCDbContext context) : base(context) {}

	public IEnumerable<Tournament> GetAllForGuild(ulong guildId) => _dbSet.Where(x => x.GuildId == guildId);
}