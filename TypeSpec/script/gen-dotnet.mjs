//@ts-check
import fs from "node:fs";
import { exec } from "child_process";
import { findNamespaces } from "./gen-common.mjs";

function main() {
  console.log("正在移除旧版本 .NET 产物");
  fs.rmSync("../Dashboard/Controllers", { recursive: true });

  const namespaces = findNamespaces();
  console.log("正在生成 .NET 控制器", namespaces);
  namespaces.forEach((ns) => {
    exec(
      `npx nswag openapi2cscontroller ` +
        `/input:"./tsp-output/@typespec/openapi3/openapi.${ns}.yaml" ` +
        `/classname:${ns} ` +
        `/namespace:CanaRails.Controllers.${ns} ` +
        `/output:"../Dashboard/Controllers/${ns}Controller.cs" ` +
        `/ControllerBaseClass:"Microsoft.AspNetCore.Mvc.Controller"`,
      (err) => {
        if (err) {
          throw err;
        }
      }
    );
  });
}

main();
