import { useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import { entryClient } from "../../../../../../clients";
import InfoItem from "../../../../../../components/info/info-item";

export default function AppEntryInfo() {
  const params = useParams();

  const query = createQuery(() => ({
    queryKey: ["app-entry", params.entryID],
    queryFn: () => entryClient.findByID(Number(params.entryID)),
    suspense: true,
  }));

  return (
    <>
      <div class="flex items-center mb-3">
        <h2 class="text-2xl font-bold flex-1">信息概览</h2>
      </div>
      <div class="grid grid-cols-1 gap-4 lg:grid-cols-3 mb-4">
        <InfoItem label="ID" value={query.data?.id} />
        <InfoItem label="入口名称" value={query.data?.name} />
        <InfoItem label="所属应用" value={query.data?.appID} />
      </div>
      <InfoItem label="简介" value={query.data?.description} />
    </>
  );
}
