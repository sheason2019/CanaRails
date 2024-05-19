using CanaRails.Adapters.DockerAdapter.Services;
using CanaRails.Database.Entities;

namespace DockerAdapter.Test;

public class DockerServiceTest
{
    [Fact]
    public async Task CreateHelloWorldContainer()
    {
        var image = new Image
        {
            ImageName = "hello-world:linux",
            Registry = "",
            App = new App
            {
                Name = "hello-world",
            },
        };
        var service = new DockerService();
        await service.CreateImageAsync(image);
        var containerId = await service.CreateContainerAsync(image);
        await service.StopContainerAsync(containerId);
        await service.RemoveContainerAsync(containerId);
    }
}
