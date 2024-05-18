using CanaRails.Controllers.AppMatcher;
using CanaRails.Database.Entities;

namespace CanaRails.Transformer;

public static class AppMatcherTransformer
{
  public static AppMatcherDTO ToDTO(this AppMatcher matcher)
  {
    return new AppMatcherDTO
    {
      Id = matcher.ID,
      Host = matcher.Host,
      AppID = matcher.App.ID,
    };
  }

  public static AppMatcher ToEntity(this AppMatcherDTO dto, App app)
  {
    return new AppMatcher
    {
      ID = dto.Id,
      Host = dto.Host,
      App = app,
    };
  }
}