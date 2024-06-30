import { List, Skeleton } from "@chakra-ui/react";
import AppHostnamesItem from "./app-hostnames-item";

export default function AppHostnamesLoading() {
  return (
    <List>
      {[...new Array(2)].map((_, index) => (
        <Skeleton key={index}>
          <AppHostnamesItem hostname="LOADING" />
        </Skeleton>
      ))}
    </List>
  );
}
