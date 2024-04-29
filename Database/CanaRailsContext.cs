using Microsoft.EntityFrameworkCore;

namespace CanaRails.Database;

public class CanaRailsContext : DbContext
{
  public DbSet<Entities.App> Apps { get; set; }
  public DbSet<Entities.Entry> Instances { get; set; }

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
