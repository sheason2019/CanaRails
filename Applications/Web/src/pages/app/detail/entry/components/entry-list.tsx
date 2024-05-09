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
            <div class="card-body">
              <div class="flex items-center">
                <div class="grow font-bold text-lg">{entry.name}</div>
                <div>ID {entry.id}</div>
              </div>
              <div>{entry.description}</div>
            </div>
          </a>
        )}
      </For>
    </div>
  );
}
