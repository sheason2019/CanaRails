using CanaRails.Database;
using CanaRails.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Ingress;

public class MatchService(
  CanaRailsContext context
)
{
  public async Task<App?> MatchApp(HttpContext httpContext)
  {
    // get request Host
    var host = httpContext.Request.Host.Host;
    // match APP by exact host name
    var matcher = await context.AppMatchers.
      Include(a => a.App).
      Where(a => a.Host.Equals(host)).
      FirstOrDefaultAsync();
    return matcher?.App;
  }

  public async Task<Entry?> MatchEntry(
    HttpContext httpContext,
    App app
  )
  {
    var entries = await context.Entries.
        Where(e => e.App.ID.Equals(app.ID)).
        Include(e => e.EntryMatchers).
        ToArrayAsync();
    Entry? defaultEntry = null;
    foreach (var entry in entries)
    {
      if (entry.Default) defaultEntry = entry;
      if (TestEntry(httpContext, entry)) return entry;
    }

    return defaultEntry;
  }

  public bool TestEntry(HttpContext httpContext, Entry entry)
  {
    // do not match when entry matcher array is empty
    // because request will match the default entry as fallback
    if (entry.EntryMatchers.Count == 0) return false;
    
    foreach (var matcher in entry.EntryMatchers)
    {
      if (httpContext.Request.Headers[matcher.Key] != matcher.Value)
      {
        return false;
      }
    }

    return true;
  }
}