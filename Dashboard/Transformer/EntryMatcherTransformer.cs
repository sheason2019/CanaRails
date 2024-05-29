using CanaRails.Controllers.EntryMatcher;
using CanaRails.Database.Entities;

namespace CanaRails.Transformer;

public static class EntryMatcherTransformer
{
  public static EntryMatcherDTO ToDTO(this EntryMatcher em)
  {
    return new EntryMatcherDTO
    {
      Key = em.Key,
      Value = em.Value,
    };
  }

  public static EntryMatcher ToEntity(this EntryMatcherDTO dto, Entry entry)
  {
    return new EntryMatcher
    {
      Key = dto.Key,
      Value = dto.Value,
    };
  }
}
