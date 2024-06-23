import { Alert, AlertIcon, Flex, Heading, Stack } from "@chakra-ui/react";
import { useMemo } from "react";
import EntryMatcherListItem from "./entry-matcher-list-item";
import EntryMatcherCreateButton from "./entry-matcher-create-button";
import useEntryDetail from "../hook/use-entry-detail";

export default function EntryMatcherList() {
  const { data } = useEntryDetail();

  const renderer = useMemo(() => {
    if (!data?.matchers.length) {
      return (
        <Alert status="warning">
          <AlertIcon />
          当前流量入口暂无匹配器
        </Alert>
      );
    }

    return (
      <>
        {data.matchers.map((item) => (
          <EntryMatcherListItem key={item.key} matcher={item} isLoaded />
        ))}
      </>
    );
  }, [data]);

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
