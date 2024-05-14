//@ts-check
import { exec } from "child_process";
import { findNamespaces } from "./gen-common.mjs";

function main() {
  const namespaces = findNamespaces();
  console.log("正在生成 .NET 控制器", namespaces);
  namespaces.forEach(
    ns => {
      exec(
        `npx nswag openapi2cscontroller ` +
        `/input:"./tsp-output/@typespec/openapi3/openapi.${ns}.yaml" ` +
        `/classname:${ns} ` +
        `/namespace:CanaRails.Controllers.${ns} ` +
        `/output:"../Manage/Controllers/${ns}Controller.cs" ` +
        `/ControllerBaseClass:"Microsoft.AspNetCore.Mvc.Controller"`,
        (err) => {
          if (err) {
            throw err;
          }
        }
      )
    },
  );
}

main();
