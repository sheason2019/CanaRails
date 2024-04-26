# CanaRails

一个简单易用的金丝雀发布平台。

# 接口生成

该项目通过 TypeSpec 定义 IDL，并生成接口供两端通信。

接口生成依赖 `https://www.npmjs.com/package/nswag`。

Server 端通过 TypeSpec 目录下的 `script/gen-dotnet.ps1` 脚本生成 Controller 文件，并在 Controller.Impl 目录下进行实现。

Web 端通过 TypeSpec 目录下的 `script/gen-web.ps1` 脚本生成 Client 文件。
