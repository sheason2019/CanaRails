import useSWR from "swr";
import { entryClient } from "../../../../../api";
import { useParams } from "react-router-dom";
import EntryListLoading from "./entry-list-loading";
import { Alert, AlertIcon, VStack } from "@chakra-ui/react";
import EntryListItem from "./entry-list-item";
import useAppDetail from "../../hooks/use-app-detail";

export default function EntryList() {
  const { appId } = useParams();
  const { data: appDetail } = useAppDetail();
  const { data, isLoading } = useSWR(["entry-list", appId], () =>
    entryClient.list(Number(appId))
  );

  if (isLoading) {
    return <EntryListLoading />;
  }

  if (!data?.length) {
    return (
      <Alert status="warning">
        <AlertIcon />
        未查询到可用的流量入口
      </Alert>
    );
  }

  return (
    <VStack alignItems="stretch" gap={4}>
      {data?.map((item) => (
        <EntryListItem
          key={item.id}
          entry={item}
          isDefault={item.id === appDetail?.defaultEntryId}
          isLoaded
        />
      ))}
    </VStack>
  );
}
