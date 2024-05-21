
using CanaRails.Controllers.EntryMatcher;
using CanaRails.Database;
using CanaRails.Transformer;
using Microsoft.EntityFrameworkCore;

namespace CanaRails.Controllers.Impl;

public class EntryMatcherControllerImpl(
  CanaRailsContext context
) : IEntryMatcherController
{
  public Task DeleteAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<ICollection<EntryMatcherDTO>> ListAsync(int entryId)
  {
    var query = from matchers in context.EntryMatchers
                where matchers.Entry.ID.Equals(entryId)
                select matchers;
    ICollection<EntryMatcherDTO> dtos = [..
      query.
        Include(e => e.Entry).
        Select(e => e.ToDTO())
    ];
    return Task.FromResult(dtos);
  }

  public Task<int> PutAsync(EntryMatcherDTO body)
  {
    using var transcation = context.Database.BeginTransaction();

    var queryEntry = from entries in context.Entries
                     where entries.ID.Equals(body.EntryId)
                     select entries;
    var entry = queryEntry.First();

    var queryExist = from matchers in context.EntryMatchers
                     where matchers.Entry.ID.Equals(body.EntryId)
                     where matchers.Key.Equals(body.Key)
                     select matchers;
    var exist = queryExist.FirstOrDefault();

    var id = 0;
    if (exist == null)
    {
      var record = new Database.Entities.EntryMatcher
      {
        Key = body.Key,
        Value = body.Value,
        Entry = entry,
      };
      context.EntryMatchers.Add(record);
      id = record.ID;
    } else {
      exist.Value = body.Value;
      id = exist.ID;
    }

    context.SaveChanges();
    transcation.Commit();

    return Task.FromResult(id);
  }
}
