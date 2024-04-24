using CanaRails.Adapters.DockerAdapter.Services;

namespace DockerAdapter.Test;

public class DockerServiceTest
{
    [Fact]
    public async Task CreateHelloWorldContainer()
    {
        var imageName = "hello-world";
        var service = new DockerService();
        await service.CreateImageAsync(imageName);
        await service.CreateContainerAsync(imageName);
    }
}
