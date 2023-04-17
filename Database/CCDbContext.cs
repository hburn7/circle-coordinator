using circle_coordinator.Database.Models;
using circle_coordinator.Database.Models.Abstractions;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618

namespace circle_coordinator.Database;

public class CCDbContext : DbContext
{
	public CCDbContext(DbContextOptions<CCDbContext> options) : base(options) {}
	public DbSet<Tournament> Tournaments { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Configure primary keys for all entities inheriting from BaseEntity
		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
		{
			if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
			{
				modelBuilder.Entity(entityType.ClrType).HasKey(nameof(BaseEntity.Id));
			}
		}

		modelBuilder.Entity<Player>()
		            .HasMany(p => p.Tournaments)
		            .WithMany(t => t.Players);

		modelBuilder.Entity<Player>()
		            .HasMany(p => p.Teams)
		            .WithMany(t => t.Players)
		            .UsingEntity<TeamPlayer>(
			            l => l.HasOne<Team>(t => t.Team).WithMany(t => t.TeamPlayers).HasForeignKey(t => t.PlayerId),
			            r => r.HasOne<Player>(p => p.Player).WithMany(p => p.TeamPlayers).HasForeignKey(p => p.TeamId));

		modelBuilder.Entity<Replay>()
		            .HasOne(r => r.Stage)
		            .WithMany(s => s.Replays)
		            .HasForeignKey(r => r.StageId);

		modelBuilder.Entity<Tournament>()
		            .HasMany<Team>(t => t.Teams)
		            .WithOne(t => t.Tournament)
		            .HasForeignKey(t => t.TournamentId);

		modelBuilder.Entity<Tournament>()
		            .HasMany<TournamentStage>(t => t.Stages)
		            .WithOne(t => t.Tournament)
		            .HasForeignKey(t => t.TournamentId);

		modelBuilder.Entity<Team>()
		            .HasMany(t => t.Players)
		            .WithMany(p => p.Teams);

		modelBuilder.Entity<TournamentStage>()
		            .HasMany<Replay>(s => s.Replays)
		            .WithOne(r => r.Stage)
		            .HasForeignKey(r => r.StageId);
	}
}