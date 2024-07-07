using Admin.Domains.Auth.Application;
using Admin.Infrastructure.ProgramSetup;
using Admin.Infrastructure.Repository;
using Admin.Infrastructure.Exceptions;
using Admin.Apis;
using Admin.Domains.Core;

var builder = WebApplication.CreateBuilder();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<CanaRailsContext>();

builder.Services.AddCanaRailsApi();
builder.Services.AddAuthDomain();
builder.Services.AddCoreDomain();
builder.Services.AddControllers(options =>
{
  options.Filters.Add<HttpStandardExceptionFilter>();
});

builder.Services.AddCanarailsKuberneters();

var app = builder.Build();

app.MapControllers();

app.SetupAuthDomain();

app.InitialApplication();
app.InitialApplication();

app.Run("http://*:8080");
