import { createMemo } from "solid-js";
import InfoItem from "../../../../components/info/info-item";
import { useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import { appClient } from "../../../../clients";

export default function InfoBlock() {
  const params = useParams();

  const app = createQuery(() => ({
    queryKey: ["query-app", params.id],
    queryFn: async () => appClient.findByID(Number(params.id)),
    suspense: true,
  }));

  const desc = createMemo(() => {
    const d = app.data?.description;
    if (!d) {
      return <span class="text-gray-400">暂无简介</span>;
    }
    return <span>{d}</span>;
  });

  return (
    <>
      <div class="grid grid-cols-1 gap-4 lg:grid-cols-3 mb-4">
        <InfoItem label="ID" value={app.data?.id} />
        <InfoItem label="名称" value={app.data?.name} />
      </div>
      <InfoItem label="简介" value={desc()} />
    </>
  );
}
