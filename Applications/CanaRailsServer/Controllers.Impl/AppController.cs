namespace CanaRails.Controllers.Impl;

public class AppControllerImpl : IAppController
{
  public Task<AppDTO> CreateAsync(Body body)
  {
    var dto = body.Dto;
    return Task.FromResult(dto);
  }

  public Task<ICollection<AppDTO>> ListAsync()
  {
    throw new NotImplementedException();
  }
}
