using circle_coordinator.Models.Entities;

namespace circle_coordinator.Data.Repositories.Interfaces;

public interface ITournamentRepository : IRepository<Tournament>
{
	public IEnumerable<Tournament> GetAllForGuild(ulong guildId);
}