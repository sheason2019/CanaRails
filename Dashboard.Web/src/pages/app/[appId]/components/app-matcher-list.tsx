import { Flex, Heading, List } from "@chakra-ui/react";
import AppMatcherListLoading from "./app-matcher-list-loading";
import { useMemo } from "react";
import AppMatcherListEmpty from "./app-matcher-list-empty";
import AppMatcherCreateButton from "./app-matcher-create-button";
import AppMatcherListItem from "./app-matcher-list-item";
import useAppDetail from "../hooks/use-app-detail";

export default function AppMatcherList() {
  const { data, isLoading } = useAppDetail();

  const listRenderer = useMemo(() => {
    if (isLoading) return <AppMatcherListLoading />;
    if (!data?.hostnames.length) return <AppMatcherListEmpty />;

    return (
      <List>
        {data.hostnames.map((item) => (
          <AppMatcherListItem key={item} hostname={item} />
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
        <AppMatcherCreateButton />
      </Flex>
      {listRenderer}
    </>
  );
}
