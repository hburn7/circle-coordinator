using circle_coordinator.Models;
using circle_coordinator.Models.Entities;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618

namespace circle_coordinator.Data.Context;

public class CCDbContext : DbContext
{
	public CCDbContext(DbContextOptions<CCDbContext> options) : base(options) {}
	public DbSet<Tournament> Tournaments { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// Configure primary keys for all entities inheriting from BaseEntity
		foreach (var entityType in modelBuilder.Model.GetEntityTypes())
		{
			if (typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
			{
				modelBuilder.Entity(entityType.ClrType).HasKey(nameof(EntityBase.Id));
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

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		var currentTime = DateTime.UtcNow;

		// Get all added and updated entries
		var addedOrUpdatedEntries = ChangeTracker.Entries()
		                                         .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified);

		foreach (var entry in addedOrUpdatedEntries)
		{
			if (entry.Entity is EntityBase entityBase)
			{
				if (entry.State == EntityState.Added)
				{
					entityBase.CreatedAt = currentTime;
				}

				entityBase.UpdatedAt = currentTime;
			}
		}

		return await base.SaveChangesAsync(cancellationToken);
	}
}