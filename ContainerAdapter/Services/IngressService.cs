using CanaRails.Database.Entities;
using k8s;
using k8s.Models;

namespace CanaRails.ContainerAdapter.Services;

public class IngressService(Kubernetes client)
{
  // 根据当前数据库记录更新主干 Ingress 路由逻辑
  public void UpdateMainIngress()
  {
    var ns = "canarails";
    var mainIngressName = "canarails-main-ingress";
    // 查询 CanaRails MainIngress 是否存在
    var ingresses = client.NetworkingV1.ListNamespacedIngress(ns);
    var mainIngressExist = ingresses.Items
      .Where(e => e.Metadata.Name.Equals(mainIngressName))
      .Any();
    if (mainIngressExist) return;

    // 当不存在时，创建基础 ingress
    var ingress = client.NetworkingV1.ReplaceNamespacedIngress(
      new V1Ingress
      {
        Metadata = {
          Name = mainIngressName,
        },
        Spec = {
          IngressClassName = "nginx-example",
          Rules = [
            // TODO: 遍历数据库中的 App 列表，将规则嵌入至此位置
            new V1IngressRule{
              Host = "",
              Http = {
                Paths = [
                  new V1HTTPIngressPath {
                    Path = "/",
                    PathType = "Prefix",
                    Backend = {
                      Service = {
                        Name = "",
                        Port = {
                          Number = 80,
                        },
                      },
                    },
                  },
                ],
              }
            },
          ],
        },
      },
      mainIngressName,
      ns
    );
  }

  public void UpdateApplicationIngress() { }
}