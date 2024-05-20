import { Alert, AlertIcon, Flex, Heading, Stack } from "@chakra-ui/react";
import EntryContainerUpdateButton from "./entry-container-update-button";
import useContainerList from "../hook/use-container-list";
import EntryContainerListLoading from "./entry-container-list-loading";
import { useMemo } from "react";
import EntryContainerlistItem from "./entry-container-list-item";

export default function EntryContainerList() {
  const { data, isLoading } = useContainerList();

  const contentRenderer = useMemo(() => {
    if (isLoading) return <EntryContainerListLoading />;
    if (!data?.length) {
      return (
        <Alert status="warning">
          <AlertIcon />
          暂无容器信息
        </Alert>
      );
    }
    return (
      <>
        {data.map((item) => (
          <EntryContainerlistItem key={item.id} container={item} isLoaded />
        ))}
      </>
    );
  }, [data, isLoading]);

  return (
    <>
      <Flex className="my-3" alignItems="center">
        <Heading size="md" className="grow">
          容器一览
        </Heading>
        <EntryContainerUpdateButton />
      </Flex>
      <Stack gap={4}>{contentRenderer}</Stack>
    </>
  );
}
