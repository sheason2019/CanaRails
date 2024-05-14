import { For } from "solid-js";
import NewMatcherDialog from "./new-matcher-dialog";
import useEntryMatcherList from "../../hooks/use-entry-matcher-list";
import EntryMatcherControl from "./entry-matcher-control";

export default function EntryMatcherList() {
  let dialog: HTMLDialogElement | undefined;

  const query = useEntryMatcherList();

  return (
    <div class="mt-4">
      <div class="flex">
        <h1 class="text-2xl font-bold grow">入口匹配器</h1>
        <button class="btn" onClick={() => dialog?.showModal()}>
          新增
        </button>
      </div>
      <div class="grid grid-cols-1 md:grid-cols-3 gap-2">
        <For each={query.data}>
          {(matcher) => (
            <div class="card shadow">
              <div class="card-body">
                <div class="flex items-center">
                  <div class="text-lg font-bold grow overflow-hidden text-ellipsis whitespace-nowrap">
                    {matcher.key}
                  </div>
                  <EntryMatcherControl entryMatcher={matcher} />
                </div>
                <div class="overflow-hidden text-ellipsis">
                  {matcher.value}
                </div>
              </div>
            </div>
          )}
        </For>
      </div>
      <NewMatcherDialog ref={dialog} />
    </div>
  );
}
