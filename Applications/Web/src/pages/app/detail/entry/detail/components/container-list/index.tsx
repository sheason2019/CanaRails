import { For } from "solid-js";
import { createQuery } from "@tanstack/solid-query";
import PutContainerButton from "../put-container-button";
import { useParams } from "@solidjs/router";
import { entryClient } from "../../../../../../../clients";

export default function ContainerList() {
  const params = useParams();
  const query = createQuery(() => ({
    queryKey: ["container-list", params.entryID],
    queryFn: () => entryClient.listContainer(Number(params.entryID)),
    suspense: true,
  }));

  return (
    <>
      <div class="flex items-center mt-4">
        <h2 class="text-2xl font-bold grow">容器列表</h2>
        <PutContainerButton />
      </div>
      <For each={query.data}>
        {(container) => (
          <div class="border mt-4">
            <div>ID: {container.id}</div>
            <div>ContainerID: {container.containerID}</div>
            <div>State: {container.state}</div>
          </div>
        )}
      </For>
    </>
  );
}
