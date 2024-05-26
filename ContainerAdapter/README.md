# 调度层

调度层用来管理应用数据和 K8s 的交互。

## 扁平化设计

Canarails 的调度层被采用了扁平化的设计，容器内的所有应用和流量入口通过一个同一的 Gateway 暴露到外部网络。

Canarails 中的持久层有三个很关键的数据类型，它们分别是：

- App

- Entry

- PublishOrder

其中 App 和 Entry 的功能是根据 Host 和 header 分发用户的请求流量，而 PublishOrder 的功能则是生成一个变更请求，用来变更实际运行的服务。

## 详细设计

调度层的设计有以下几个需要注意的点：

1. 当 App 和 Entry 的配置发生变化时，我们需要拉取应用中全部的 App 和 Entry 记录，并修改 HTTPRoute 以实现网关层的更新。

2. 当我们创建 Entry 时，我们需要同步创建一个 K8s Service，并将指向该 Entry 的流量路由到该 Service 上。

3. 当我们应用一个 PublishOrder 时，我们需要变更 Service 中的 Deployment，从而实现 Entry 应用的更新。
