/* @refresh reload */
import { render } from "solid-js/web";

import "./index.css";
import App from "./App";
import { QueryClientProvider } from "@tanstack/solid-query";
import { QueryClient } from "@tanstack/query-core";

const root = document.getElementById("root");

const queryClient = new QueryClient();

render(
  () => (
    <QueryClientProvider client={queryClient}>
      <App />
    </QueryClientProvider>
  ),
  root!
);
