import { useParams } from "@solidjs/router";

export default function AppStats() {
  const params = useParams();

  return (
    <>
      <h2 class="mt-4 mb-2 text-2xl font-bold">数据信息</h2>
      <p class="text-sm text-gray-500">点击下列卡片可前往相关页面</p>
      <div class="grid grid-cols-1 lg:grid-cols-3 gap-4">
        <a
          class="stats shadow-lg hover:shadow-xl transition-shadow mt-4"
          href={`/app/${params.id}/instance`}
        >
          <div class="stat">
            <div class="stat-title">应用实例</div>
            <div class="stat-value">0</div>
            <div class="stat-desc">配置主干和支线应用</div>
          </div>
        </a>
        <a
          class="stats shadow-lg hover:shadow-xl transition-shadow mt-4"
          href={`/app/${params.id}/log`}
        >
          <div class="stat">
            <div class="stat-title">应用日志</div>
            <div class="stat-value">0</div>
            <div class="stat-desc">查看该应用的日志信息</div>
          </div>
        </a>
      </div>
    </>
  );
}
