import { List, Skeleton } from "@chakra-ui/react";
import { AppMatcherDTO } from "../../../../../api-client/AppMatcher.client";
import AppMatcherListItem from "./app-matcher-list-item";

const LOADING_MATCHER: AppMatcherDTO = {
  id: 0,
  host: "LOADING",
  appID: 0,
};

export default function AppMatcherListLoading() {
  return (
    <List>
      {[...new Array(2)].map((_, index) => (
        <Skeleton key={index}>
          <AppMatcherListItem appMatcher={LOADING_MATCHER} />
        </Skeleton>
      ))}
    </List>
  );
}
