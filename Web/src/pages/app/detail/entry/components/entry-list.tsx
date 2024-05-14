import { For } from "solid-js";
import EntryCard from "./entry-card";
import useEntryList from "../hooks/use-entry-list";

export default function EntryList() {
  const query = useEntryList();

  return (
    <>
      <h2 class="text-2xl mt font-bold">入口列表</h2>
      <div class="grid grid-cols-1 mt-4">
        <For each={query.data}>{(entry) => <EntryCard entry={entry} />}</For>
      </div>
    </>
  );
}
