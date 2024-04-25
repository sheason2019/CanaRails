using CanaRails.Protos;
using Grpc.Core;

namespace CanaRails.Applications.Server.Services;

public class AppService(ILogger<AppService> logger) : Protos.AppService.AppServiceBase
{
    public override async Task<CreateAppReply> CreateApp(CreateAppRequest request, ServerCallContext context)
    {
        logger.LogInformation("Create App");
        return new CreateAppReply
        {

        };
    }
}
