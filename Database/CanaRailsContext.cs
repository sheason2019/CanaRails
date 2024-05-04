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

  public string DbPath { get; }

  public CanaRailsContext()
  {
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    DbPath = Path.Join(path, "cana-rails.db");
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite($"Data Source={DbPath}");
  }
}
