using CanaRails.Database.Entities;
using k8s;
using k8s.Models;

namespace CanaRails.ContainerAdapter.Services;

public class OrderService(Kubernetes client)
{
  public async Task Up(PublishOrder order)
  {
    // 
  }
  public async Task Down(PublishOrder order) { }
}