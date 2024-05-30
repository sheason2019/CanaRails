using System.Text.Json;
using CanaRails.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CanaRails.Database;

public class CanaRailsContext : DbContext
{
  public DbSet<App> Apps { get; set; }
  public DbSet<Entry> Entries { get; set; }
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


    modelBuilder.Entity<App>()
      .Property(e => e.Hostnames)
      .HasConversion(
        v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
        v => JsonSerializer.Deserialize<List<string>>(v, JsonSerializerOptions.Default) ?? new List<string>(),
        new ValueComparer<ICollection<string>>(
          (c1, c2) => (c1 ?? new List<string>()).SequenceEqual((c2 ?? new List<string>())),
          c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
          c => (ICollection<string>)c.ToList()));

    modelBuilder.Entity<Entry>().OwnsMany(
        e => e.EntryMatchers, builder =>
        {
          builder.ToJson();
        }
    );
  }
}
