import { For } from "solid-js";
import { createQuery } from "@tanstack/solid-query";
import PutContainerButton from "./put-container-button";
import { useParams } from "@solidjs/router";
import { entryClient } from "../../../../../../clients";

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
      <div class="grid grid-cols-1 gap-4 mt-4">
        <For each={query.data}>
          {(container) => (
            <div class="card shadow hover:shadow-lg transition-shadow">
              <div class="card-body">
                <div class="flex">
                  <div class="grow">ID: {container.id}</div>
                  <div
                    classList={{
                      "text-gray-400": container.state === "exited",
                      "text-green-500": container.state === "running",
                    }}
                  >
                    {mapStateName(container.state)}
                  </div>
                </div>
                <div class="text-sm text-gray-500">容器ID {container.containerID}</div>
              </div>
            </div>
          )}
        </For>
      </div>
    </>
  );
}

function mapStateName(state: string | undefined) {
  switch (state) {
    case "exited":
      return "已退出";
    case "running":
      return "运行中";
    default:
      return state;
  }
}
