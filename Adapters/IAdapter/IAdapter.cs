﻿using CanaRails.Database.Entities;

namespace CanaRails.Adapters.IAdapter;

public interface IAdapter
{
  public Task PullImage(Image image);
  public Task DeleteImage(Image image);
  public Task<string> CreateContainer(Image image);
  public Task StopContainer(string containerId);
  public Task RestartContainer(string containerId);
  public Task RemoveContainer(string containerId);
  public Task<string[]> GetContainerState(string[] containerIds);
  public Task<string> GetContainerIP(string containerId);
}