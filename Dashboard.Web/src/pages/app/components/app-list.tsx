import { Stack } from "@chakra-ui/react";
import useSWR from "swr";
import { appClient } from "../../../api";
import AppListLoading from "./app-list-loading";
import { useMemo } from "react";
import AppListEmpty from "./app-list-empty";
import AppListItem from "./app-list-item";

export default function AppList() {
  const { data, isLoading } = useSWR(["app-list"], async () =>
    appClient.list()
  );

  const renderer = useMemo(() => {
    if (isLoading) {
      return <AppListLoading />;
    }

    if (data?.length === 0) {
      return <AppListEmpty />;
    }

    return (
      <>
        {data?.map((item) => (
          <AppListItem key={item.id} app={item} isLoaded />
        ))}
      </>
    );
  }, [data, isLoading]);

  return (
    <Stack className="mt-6" gap={4}>
      {renderer}
    </Stack>
  );
}
