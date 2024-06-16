import AppListItem from "./app-list-item";
import { AppDTO } from "../../../../api-client/App.client";

const EMPTY_APP: AppDTO = {
  id: 0,
  name: "Empty Name",
  description: "Empty Description",
  hostnames: [],
  defaultEntryId: 0,
};

export default function AppListLoading() {
  return (
    <>
      {[...new Array(6)].map((_, index) => (
        <AppListItem app={EMPTY_APP} key={index} isLoaded={false} />
      ))}
    </>
  );
}
