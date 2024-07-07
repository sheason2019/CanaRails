using Admin.Domains.Core.Models.Entities;
using Admin.Infrastructure.IDL;

namespace CanaRails.Transformer;

public static class EntryTransformer
{
  public static EntryDTO ToDTO(this Entry entry)
  {
    return new EntryDTO
    {
      Id = entry.ID,
      Name = entry.Name,
      Description = entry.Description,
      Matchers = entry
        .EntryMatchers
        .Select(e => new EntryMatcherDTO
        {
          Key = e.Key,
          Value = e.Value,
        })
        .ToList(),
      AppId = entry.App.ID,
    };
  }

  public static Entry ToEntity(this EntryDTO dto, App app)
  {
    return new Entry
    {
      ID = dto.Id,
      Name = dto.Name,
      Description = dto.Description,
      EntryMatchers = dto
        .Matchers
        .Select(e => new EntryMatcher
        {
          Key = e.Key,
          Value = e.Value,
        })
        .ToList(),
      App = app,
    };
  }
}
