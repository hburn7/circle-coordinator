using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace circle_coordinator.Data.Context;

/// <summary>
/// Runs migrations on the database at design time. This is used by the EF Core CLI tools.
/// Required, if not implemented, the bot will run as if `dotnet run` was used.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CCDbContext>
{
	public CCDbContext CreateDbContext(string[] args)
	{
		IConfigurationRoot configuration = new ConfigurationBuilder()
		                                   .SetBasePath(Directory.GetCurrentDirectory())
		                                   .AddJsonFile("appsettings.json")
		                                   .Build();

		string? connectionString = configuration.GetConnectionString("DefaultConnection");

		var builder = new DbContextOptionsBuilder<CCDbContext>();
		builder.UseNpgsql(connectionString);

		return new CCDbContext(builder.Options);
	}
}