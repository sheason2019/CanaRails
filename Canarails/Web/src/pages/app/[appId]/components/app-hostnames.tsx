import { Flex, Heading, List } from "@chakra-ui/react";
import AppHostnamesLoading from "./app-hostnames-loading";
import { useMemo } from "react";
import AppHostnamesEmpty from "./app-hostnames-empty";
import AppHostnamesCreateButton from "./app-hostnames-create-button";
import AppHostnamesItem from "./app-hostnames-item";
import useAppDetail from "../hooks/use-app-detail";

export default function AppHostnames() {
  const { data, isLoading } = useAppDetail();

  const listRenderer = useMemo(() => {
    if (isLoading) return <AppHostnamesLoading />;
    if (!data?.hostnames.length) return <AppHostnamesEmpty />;

    return (
      <List>
        {data.hostnames.map((item) => (
          <AppHostnamesItem key={item} hostname={item} />
        ))}
      </List>
    );
  }, [isLoading, data]);

  return (
    <>
      <Flex className="mt-6 mb-3 items-center">
        <Heading size="md" className="grow">
          App Hostnames
        </Heading>
        <AppHostnamesCreateButton />
      </Flex>
      {listRenderer}
    </>
  );
}
