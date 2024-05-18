import { Flex, Heading, List } from "@chakra-ui/react";
import useAppMatchers from "../hooks/use-app-matchers";
import AppMatcherListLoading from "./app-matcher-list-loading";
import { useMemo } from "react";
import AppMatcherListEmpty from "./app-matcher-list-empty";
import AppMatcherCreateButton from "./app-matcher-create-button";
import AppMatcherListItem from "./app-matcher-list-item";

export default function AppMatcherList() {
  const { data, isLoading } = useAppMatchers();

  const listRenderer = useMemo(() => {
    if (isLoading) return <AppMatcherListLoading />;
    if (!data?.length) return <AppMatcherListEmpty />;

    return (
      <List>
        {data.map((item) => (
          <AppMatcherListItem key={item.id} appMatcher={item} />
        ))}
      </List>
    );
  }, [isLoading, data]);

  return (
    <>
      <Flex className="mt-6 mb-3 items-center">
        <Heading size="lg" className="grow">
          匹配器
        </Heading>
        <AppMatcherCreateButton />
      </Flex>
      {listRenderer}
    </>
  );
}
