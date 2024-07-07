import { Alert, AlertIcon, Box, Flex, Heading, Stack } from "@chakra-ui/react";
import PublishOrderCreateButton from "./publish-order-create-button";
import usePublishOrderList from "../hook/use-publish-order-list";
import PublishOrderListLoading from "./publish-order-list-loading";
import { useMemo } from "react";
import PublishOrderListItem from "./publish-order-list-item";

export default function EntryVersionList() {
  const { data, isLoading } = usePublishOrderList();

  const contentRenderer = useMemo(() => {
    if (isLoading) return <PublishOrderListLoading />;
    if (!data?.length) {
      return (
        <Alert status="warning">
          <AlertIcon />
          <Box>当前流量入口无版本发布记录</Box>
        </Alert>
      );
    }
    return (
      <>
        {data.map((item) => (
          <PublishOrderListItem key={item.id} publishOrder={item} isLoaded />
        ))}
      </>
    );
  }, [data, isLoading]);

  return (
    <>
      <Flex className="my-3" alignItems="center">
        <Heading size="md" className="grow">
          版本发布记录
        </Heading>
        <PublishOrderCreateButton />
      </Flex>
      <Stack gap={4}>{contentRenderer}</Stack>
    </>
  );
}
