using Admin.Domains.Core.Models.Entities;
using Admin.Infrastructure.IDL;

namespace Admin.Domains.Core.Factories;

public class DtoFactory
{
  public AppDTO CreateAppDTO(App app)
  {
    return new AppDTO
    {
      Id = app.ID,
      Name = app.Name,
      Description = app.Description,
      Hostnames = app.Hostnames,
      DefaultEntryId = app.DefaultEntryId,
    };
  }

  public EntryDTO CreateEntryDTO(Entry entry)
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

  public ImageDTO CreateImageDTO(Image image)
  {
    return new ImageDTO
    {
      Id = image.ID,
      AppId = image.App.ID,
      ImageName = image.ImageName,
      CreatedAt = ((DateTimeOffset)image.CreatedAt).ToUnixTimeMilliseconds(),
    };
  }
}
