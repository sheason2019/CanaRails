import { List, Skeleton } from "@chakra-ui/react";
import AppMatcherListItem from "./app-matcher-list-item";

export default function AppMatcherListLoading() {
  return (
    <List>
      {[...new Array(2)].map((_, index) => (
        <Skeleton key={index}>
          <AppMatcherListItem hostname="LOADING" />
        </Skeleton>
      ))}
    </List>
  );
}
