import { createQuery } from "@tanstack/solid-query";
import { appClient } from "../../../clients";
import { For, Show } from "solid-js";

export default function AppList() {
  const appListQuery = createQuery(() => ({
    queryKey: ["app-list"],
    queryFn: () => appClient.list(),
    suspense: true,
  }));

  return (
    <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4 mt-4">
      <For each={appListQuery.data ?? []}>
        {(app) => (
          <a href={`/app/${app.id}`}>
            <div class="card bg-base-100 shadow-md">
              <div class="card-body">
                <h2 class="card-title">{app.name}</h2>
                <p class="text-sm text-gray-500">ID: {app.id}</p>
                <Show when={app.description}>
                  <p>{app.description}</p>
                </Show>
              </div>
            </div>
          </a>
        )}
      </For>
    </div>
  );
}
