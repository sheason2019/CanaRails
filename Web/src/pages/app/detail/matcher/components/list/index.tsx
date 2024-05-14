import useAppMatcherListQuery from "./hooks/use-app-matcher-list-query";
import { For } from "solid-js";

export default function AppMatcherList() {
  const query = useAppMatcherListQuery();

  return (
    <div>
      <For each={query.data}>
        {(item) => (
          <div class="border mt-4">
            <div>ID: {item.id}</div>
            <div>HOST: {item.host}</div>
          </div>
        )}
      </For>
    </div>
  );
}
