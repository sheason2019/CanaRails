using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Admin.Infrastructure.Constants;
using Admin.Domains.Core.Models.Entities;

namespace Admin.Infrastructure.Repository;

public class CanaRailsContext : IdentityDbContext<IdentityUser>, IDataProtectionKeyContext
{
  public DbSet<App> Apps { get; set; }
  public DbSet<Entry> Entries { get; set; }
  public DbSet<EntryVersion> EntryVersions { get; set; }
  public DbSet<Image> Images { get; set; }

  public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseNpgsql(
        $"Host={EnvVariables.CANARAILS_DBHOST};" +
        $"Database={EnvVariables.CANARAILS_DBNAME};" +
        $"Username={EnvVariables.CANARAILS_DBUSER};" +
        $"Password={EnvVariables.CANARAILS_DBPSWD}"
    );
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<App>()
      .HasOne(e => e.DefaultEntry)
      .WithOne();

    modelBuilder.Entity<App>()
      .Property(e => e.Hostnames)
      .HasConversion(
        v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
        v => JsonSerializer.Deserialize<List<string>>(v, JsonSerializerOptions.Default) ?? new List<string>(),
        new ValueComparer<ICollection<string>>(
          (c1, c2) => (c1 ?? new List<string>()).SequenceEqual(c2 ?? new List<string>()),
          c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
          c => c.ToList()));

    modelBuilder.Entity<Entry>().OwnsMany(
        e => e.EntryMatchers, builder =>
        {
          builder.ToJson();
        }
    );
  }
}
