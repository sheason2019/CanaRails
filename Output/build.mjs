//@ts-check

import { execSync } from "node:child_process";
import { existsSync, mkdirSync, rmSync } from "node:fs";

function build() {
  const distPath = "./dist";
  if (existsSync(distPath)) {
    rmSync(distPath, { recursive: true });
  }

  mkdirSync(distPath);

  console.info("正在构建 Web 静态资源");
  execSync(
    `
    cd ../Applications/Web
    npm run build
    mv dist ../../Output/dist/static
    `,
    {
      stdio: "inherit",
    }
  );

  console.info("正在构建 .NET 服务端");
  execSync(
    `
    cd ../Applications/Integration
    dotnet build --configuration Release -o ../../Output/dist
    `,
    {
      stdio: "inherit",
    }
  );
}

build();
