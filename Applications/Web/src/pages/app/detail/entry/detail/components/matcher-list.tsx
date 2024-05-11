import { useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import { entryClient } from "../../../../../../clients";
import { For } from "solid-js";
import NewMatcherDialog from "./new-matcher-dialog";

export default function EntryMatcherList() {
  const params = useParams();
  let dialog: HTMLDialogElement | undefined;

  const query = createQuery(() => ({
    queryKey: ["entry-matcher-list", params.entryID],
    queryFn: () => entryClient.listMatcher(Number(params.entryID)),
    suspense: true,
  }));

  return (
    <div class="mt-4">
      <div class="flex">
        <h1 class="text-2xl font-bold grow">入口匹配器</h1>
        <button class="btn" onClick={() => dialog?.showModal()}>
          新增
        </button>
      </div>
      <For each={query.data}>
        {(matcher) => (
          <div>
            <div>ID {matcher.id}</div>
            <div>KEY {matcher.key}</div>
            <div>VALUE {matcher.value}</div>
          </div>
        )}
      </For>
      <NewMatcherDialog ref={dialog} />
    </div>
  );
}
