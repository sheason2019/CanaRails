import { useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import { JSX } from "solid-js";
import { entryClient, imageClient } from "../../../../clients";

export default function AppStats() {
  const params = useParams();

  const countQuery = createQuery(() => ({
    queryKey: ["query-stats"],
    queryFn: async () => {
      const entryCount = await entryClient.count(Number(params.id));
      const imageCount = await imageClient.count(Number(params.id));

      return {
        entry: entryCount,
        iamge: imageCount,
      };
    },
  }));

  return (
    <>
      <h2 class="mt-4 mb-2 text-2xl font-bold">数据信息</h2>
      <p class="text-sm text-gray-500">点击下列卡片可前往相关页面</p>
      <div class="grid grid-cols-1 lg:grid-cols-3 gap-4">
        <StatComp
          href={`/app/${params.id}/entry`}
          label="入口"
          value={countQuery.data?.entry ?? "0"}
          desc="配置应用入口"
        />
        <StatComp
          href={`/app/${params.id}/image`}
          label="可用镜像"
          value={countQuery.data?.iamge ?? "0"}
          desc="查看和管理可用镜像"
        />
        <StatComp
          href={`/app/${params.id}/matcher`}
          label="匹配器"
          value="0"
          desc="查看和管理可匹配到当前应用的访问地址"
        />
      </div>
    </>
  );
}

function StatComp(props: {
  href: string;
  label: JSX.Element;
  value: JSX.Element;
  desc: JSX.Element;
}) {
  return (
    <a
      class="stats shadow hover:shadow-xl transition-shadow mt-4"
      href={props.href}
    >
      <div class="stat">
        <div class="stat-title">{props.label}</div>
        <div class="stat-value">{props.value}</div>
        <div class="stat-desc mt-2">{props.desc}</div>
      </div>
    </a>
  );
}
