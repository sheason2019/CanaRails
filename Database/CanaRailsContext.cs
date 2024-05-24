using CanaRails.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Database;

public class CanaRailsContext : DbContext
{
    public DbSet<App> Apps { get; set; }
    public DbSet<AppMatcher> AppMatchers { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<EntryMatcher> EntryMatchers { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Container> Containers { get; set; }
    public DbSet<PublishOrder> PublishOrders { get; set; }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entry>()
            .HasOne(e => e.CurrentPublishOrder)
            .WithOne(e => e.Entry)
            .HasForeignKey<PublishOrder>();

        modelBuilder.Entity<App>()
            .HasOne(e => e.DefaultEntry)
            .WithOne(e => e.App)
            .HasForeignKey<Entry>();
    }
}
