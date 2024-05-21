import { Alert, AlertIcon, Flex, Heading, Stack } from "@chakra-ui/react";
import EntryMatcherListLoading from "./entry-matcher-list-loading";
import useEntryMatcherList from "../hook/use-entry-matcher-list";
import { useMemo } from "react";
import EntryMatcherListItem from "./entry-matcher-list-item";
import EntryMatcherCreateButton from "./entry-matcher-create-button";

export default function EntryMatcherList() {
  const { isLoading, data } = useEntryMatcherList();

  const renderer = useMemo(() => {
    if (isLoading) return <EntryMatcherListLoading />;

    if (!data?.length) {
      return (
        <Alert status="warning">
          <AlertIcon />
          当前流量入口暂无匹配器
        </Alert>
      );
    }

    return (
      <>
        {data.map((item) => (
          <EntryMatcherListItem key={item.id} matcher={item} isLoaded />
        ))}
      </>
    );
  }, [data, isLoading]);

  return (
    <>
      <Flex>
        <Heading size="md" className="my-3 grow">
          匹配器
        </Heading>
        <EntryMatcherCreateButton />
      </Flex>
      <Stack gap={2}>{renderer}</Stack>
    </>
  );
}
