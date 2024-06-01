//@ts-check
import fs from "node:fs";
import { exec } from "child_process";
import { findNamespaces } from "./gen-common.mjs";

function main() {
  console.log("正在移除旧版本 Web 产物");
  fs.rmSync("../Dashboard.Web/api-client", { recursive: true });

  const namespaces = findNamespaces();
  console.log("正在生成 Web 请求库", namespaces);
  namespaces.forEach((ns) => {
    const child = exec(
      `npx nswag openapi2tsclient ` +
        `/input:"./tsp-output/@typespec/openapi3/openapi.${ns}.yaml" ` +
        `/output:"../Dashboard.Web/api-client/${ns}.client.ts" ` +
        `/ClassName:"${ns}Client" ` +
        `/TypeStyle:Interface`,
      (err) => {
        if (err) {
          throw err;
        }
      }
    );
  });
}

main();
