import { useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import { entryClient } from "../../../../../clients";
import { For } from "solid-js";

export default function EntryList() {
  const params = useParams();

  const query = createQuery(() => ({
    queryKey: ["query-entry-list", params.id],
    queryFn: () => entryClient.list(Number(params.id)),
    suspense: true,
  }));

  return (
    <div class="grid grid-cols-1 mt-4">
      <For each={query.data}>
        {(entry) => (
          <a
            class="card shadow hover:shadow-lg transition-shadow"
            href={`/app/${params.id}/entry/${entry.id}`}
          >
            <div class="card-body pb-4">
              <div class="flex items-center font-bold">
                <div class="grow text-lg">{entry.name}</div>
                <div class="text-gray-400">ID {entry.id}</div>
              </div>
              <div>{entry.description}</div>
            </div>
            <div class="card-body py-3 bg-base-200">
              <div class="grid grid-cols-3 items-center">
                <div class="text-left">
                  上次部署 {new Date().toLocaleString()}
                </div>
                <div class="text-center">运行中</div>
                <div class="text-right">
                  <button
                    class="btn btn-sm btn-primary"
                    onclick={(e) => e.preventDefault()}
                  >
                    操作
                  </button>
                </div>
              </div>
            </div>
          </a>
        )}
      </For>
    </div>
  );
}
