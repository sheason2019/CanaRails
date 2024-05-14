import { createQuery } from "@tanstack/solid-query";
import { imageClient } from "../../../../../clients";
import { useParams } from "@solidjs/router";
import { For } from "solid-js";

export default function AppImageList() {
  const params = useParams();

  const imageListQuery = createQuery(() => ({
    queryKey: ["app-list"],
    queryFn: () => imageClient.list(Number(params.id)),
    suspense: true,
  }));

  return (
    <div>
      <div class="overflow-x-auto bg-base-200 mt-4 rounded-lg">
        <table class="table">
          <thead>
            <tr>
              <th>ID</th>
              <th>镜像名称</th>
              <th>镜像标签</th>
              <th>镜像仓库</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <For each={imageListQuery.data}>
              {(record) => (
                <tr>
                  <th>{record.id}</th>
                  <td>{record.imageName}</td>
                  <td>{record.tagName}</td>
                  <td>{record.registry}</td>
                  <td>
                    <button class="btn btn-primary btn-sm">删除</button>
                  </td>
                </tr>
              )}
            </For>
          </tbody>
        </table>
      </div>
    </div>
  );
}
