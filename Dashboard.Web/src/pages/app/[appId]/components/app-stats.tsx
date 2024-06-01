import { Heading } from "@chakra-ui/react";
import { useParams } from "react-router-dom";
import AppStatItem from "./app-stat-item";

export default function AppStats() {
  const { appId } = useParams();

  return (
    <>
      <Heading size="md" className="mt-6 mb-3">
        应用数据
      </Heading>
      <p className="mb-3 text-sm text-gray-500">点击卡片查看数据详情</p>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        <AppStatItem to={`/app/${appId}/entry`} label="流量入口" value="0" />
        <AppStatItem to={`/app/${appId}/image`} label="可用镜像" value="0" />
        <AppStatItem to={`/app/${appId}/log`} label="操作日志" value="0" />
      </div>
    </>
  );
}
