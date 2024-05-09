//@ts-check
import fs from "node:fs";

export function findNamespaces() {
  const files = fs.readdirSync("./tsp-output/@typespec/openapi3");
  const regexp = /openapi\.(\w+)\.yaml/;
  return files.map(file => regexp.exec(file)?.at(1));
}
