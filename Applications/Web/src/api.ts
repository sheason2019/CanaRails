import { createPromiseClient } from "@connectrpc/connect";
import { createGrpcWebTransport } from "@connectrpc/connect-web";
import { AppService } from "../proto/app_connect";

const transport = createGrpcWebTransport({
  baseUrl: "/",
});

export const appApi = createPromiseClient(AppService, transport);
