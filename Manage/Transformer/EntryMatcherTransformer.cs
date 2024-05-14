using CanaRails.Controllers.Entry;
using CanaRails.Database.Entities;

namespace CanaRails.Transformer;

public static class EntryMatcherTransformer
{
  public static EntryMatcherDTO ToDTO(this EntryMatcher em)
  {
    return new EntryMatcherDTO
    {
      Id = em.ID,
      Key = em.Key,
      Value = em.Value,
      EntryID = em.Entry.ID,
    };
  }

  public static EntryMatcher ToEntity(this EntryMatcherDTO dto, Entry entry)
  {
    return new EntryMatcher
    {
      ID = dto.Id,
      Key = dto.Key,
      Value = dto.Value,
      Entry = entry,
    };
  }
}
