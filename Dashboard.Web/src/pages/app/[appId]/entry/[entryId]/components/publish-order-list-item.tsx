import { Card, CardBody, Flex, Skeleton } from "@chakra-ui/react";
import { PublishOrderDTO } from "../../../../../../../api-client/PublishOrder.client";

interface Props {
  publishOrder: PublishOrderDTO;
  isLoaded: boolean;
}

export default function PublishOrderListItem({
  publishOrder,
  isLoaded,
}: Props) {
  return (
    <Card>
      <Skeleton isLoaded={isLoaded}>
        <CardBody>
          <Flex gap={2} alignItems="baseline">
            <p className="font-bold">ID {publishOrder.id}</p>
            <p className="text-sm text-gray-500">
              创建时间 {new Date(publishOrder.createdAt!).toLocaleString()}
            </p>
          </Flex>
          <p>
            <span>镜像 {publishOrder.imageId}</span>
            <span> · </span>
            <span>映射端口 {publishOrder.port}</span>
            <span> · </span>
            <span>实例数量 {publishOrder.replica}</span>
            <span> · </span>
            <span>当前状态 {publishOrder.status}</span>
          </p>
        </CardBody>
      </Skeleton>
    </Card>
  );
}
