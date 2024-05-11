import { Show } from "solid-js";
import EntryCard from "./entry-card";
import useEntryDefault from "../hooks/use-entry-default";

export default function EntryDefault() {
  const query = useEntryDefault();

  return (
    <Show when={query.data}>
      <div class="mb-4">
        <h2 class="text-2xl font-bold mb-4">默认入口</h2>
        <EntryCard entry={query.data!} />
      </div>
    </Show>
  );
}
