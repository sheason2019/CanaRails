using Microsoft.EntityFrameworkCore;

namespace CanaRails.Database;

public class CanaRailsContext : DbContext
{
    public DbSet<Entities.App> Apps { get; set; }
    public DbSet<Entities.AppMatcher> AppMatchers { get; set; }
    public DbSet<Entities.Entry> Entries { get; set; }
    public DbSet<Entities.EntryMatcher> EntryMatchers { get; set; }
    public DbSet<Entities.Image> Images { get; set; }
    public DbSet<Entities.Container> Containers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var host = Environment.GetEnvironmentVariable("Host");
        var dbname = Environment.GetEnvironmentVariable("Database");
        var username = Environment.GetEnvironmentVariable("Username");
        var password = Environment.GetEnvironmentVariable("Password");

        optionsBuilder.UseNpgsql(
            $"Host={host};" +
            $"Database={dbname};" +
            $"Username={username};" +
            $"Password={password}"
        );
    }
}
