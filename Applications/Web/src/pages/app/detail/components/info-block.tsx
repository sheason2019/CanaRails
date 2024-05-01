import { JSX, createMemo } from "solid-js";
import useApp from "../hooks/use-app";

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

function InfoItem(props: { label: string; value: JSX.Element }) {
  return (
    <div>
      <div class="font-bold mb-1">{props.label}</div>
      <div class="text-gray-500 text-sm">{props.value}</div>
    </div>
  );
}
