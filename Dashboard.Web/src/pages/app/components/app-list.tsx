import { Stack } from "@chakra-ui/react";
import AppListLoading from "./app-list-loading";
import { useMemo } from "react";
import AppListEmpty from "./app-list-empty";
import AppListItem from "./app-list-item";
import useAppList from "../hooks/use-app-list";

export default function AppList() {
  const { data, isLoading } = useAppList();

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
