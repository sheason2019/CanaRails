# CanaRails

一个简单易用的金丝雀发布平台。

# 接口生成

该项目通过 TypeSpec 定义 IDL，并生成接口供两端通信。

接口生成依赖 `https://www.npmjs.com/package/nswag`。

Server 端通过 TypeSpec 目录下的 `script/gen-dotnet.ps1` 脚本生成 Controller 文件，并在 Controller.Impl 目录下进行实现。

Web 端通过 TypeSpec 目录下的 `script/gen-web.ps1` 脚本生成 Client 文件。

## 启动项目

CanaRails 依赖于 Postgres 数据库，在启动项目前需要设置以下环境变量：

```pwsh
# Powershell
$env:CANARAILS_DBHOST="<host>:<port>"
$env:CANARAILS_DBNAME="<db_name>"
$env:CANARAILS_DBUSER="<db_username>"
$env:CANARAILS_DBPSWD="<db_password>"
```

```sh
# bash
export CANARAILS_DBHOST="<host>:<port>"
export CANARAILS_DBNAME="<db_name>"
export CANARAILS_DBUSER="<db_username>"
export CANARAILS_DBPSWD="<db_password>"
```

# 应用分层

CanaRails 的设计可以分为三个主要层级：

- 持久层

  主要为 Dao 层。

- 适配层

  主要通过 K8s Api 调度容器。

- 应用层

  - Dashboard

    CanaRails 的管理界面，通过一个 Web 页面和一组经过抽象设计的 Api，为用户提供自动化启停测试环境的能力。

  - Ingress

    根据用户请求中的特征，动态分发用户的请求流量，从而实现分支测试环境。

# 部署服务

1. 使用下面的命令安装 K3s，并禁用默认的 Traefik Ingress

   ```bash
   sudo curl -sfL https://rancher-mirror.rancher.cn/k3s/k3s-install.sh | INSTALL_K3S_MIRROR=cn sh -s - --disable=traefik
   ```

2. 安装 Istio

   推荐使用 Helm 安装，K3s 与 Helm 集成可参考 https://docs.k3s.io/zh/helm?_highlight=hel。

   参考 https://istio.io/latest/zh/docs/setup/install/helm/

3. 安装 Gateway API

   参考 https://istio.io/latest/zh/docs/tasks/traffic-management/ingress/gateway-api/

4. 创建 Canarails 命名空间

   ```bash
   kubectl create namespace canarails
   ```

为 containerd 配置国内镜像源，以在无网络代理的环境下拉取 istiod 镜像
https://docs.rancher.cn/docs/k3s/advanced/_index#%E9%85%8D%E7%BD%AE-containerd

资源受限环境下指定 istiod 需求的资源，以避免 istiod Deployment 被 k3s 驱逐。
```bash
istioctl install --set profile=minimal --set values.pilot.resources.requests.memory=512Mi -y
```
