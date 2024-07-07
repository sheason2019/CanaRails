using Admin.Infrastructure.IDL;

namespace Admin.Apis;

public static class Setup
{
  public static void AddCanaRailsApi(this IServiceCollection services)
  {
    services.AddScoped<IAppController, AppControllerImpl>();
    services.AddScoped<IImageController, ImageControllerImpl>();
    services.AddScoped<IEntryController, EntryControllerImpl>();
    services.AddScoped<IAuthController, AuthControllerImpl>();
    services.AddScoped<IEntryVersionController, EntryVersionControllerImpl>();
  }
}
