//@ts-check

import { execSync } from "node:child_process";
import { existsSync, mkdirSync, rmSync, cpSync } from "node:fs";
import path from "node:path";

function build() {
  const distPath = "./dist";
  if (existsSync(distPath)) {
    rmSync(distPath, { recursive: true });
  }

  mkdirSync(distPath);
  mkdirSync(path.join(distPath, "wwwroot"));

  console.info("正在为项目生成模板代码");
  execSync("npm i", {
    cwd: "../TypeSpec",
    stdio: "inherit",
  });
  execSync("npm run compile", {
    cwd: "../TypeSpec",
    stdio: "inherit",
  });
  execSync("npm run gen", {
    cwd: "../TypeSpec",
    stdio: "inherit",
  });

  console.info("正在构建 Web 静态资源");
  execSync("npm i", {
    cwd: "../Applications/Web",
    stdio: "inherit",
  });
  execSync("npm run build", {
    cwd: "../Applications/Web",
    stdio: "inherit",
  });

  console.log("将 Web 静态资源拷贝至 wwwroot 目录");
  cpSync("../Applications/Web/dist", "./dist/wwwroot", { recursive: true });

  console.info("正在构建 .NET 服务端");
  execSync("dotnet build --configuration Release -o ../../Output/dist", {
    cwd: "../Applications/Integration",
    stdio: "inherit",
  });
}

build();
