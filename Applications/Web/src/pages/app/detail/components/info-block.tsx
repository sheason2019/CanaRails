import { createMemo } from "solid-js";
import useApp from "../hooks/use-app";
import InfoItem from "../../../../components/info/info-item";

export default function InfoBlock() {
  const app = useApp();

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
