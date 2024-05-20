import { ContainerDTO } from "../../../../../../../api-client/Container.client";
import EntryContainerlistItem from "./entry-container-list-item";

const LOADING_CONTAINER: ContainerDTO = {
  id: 0,
  imageId: 0,
  entryId: 0,
  version: 0,
  port: 0,
};

export default function EntryContainerListLoading() {
  return (
    <>
      {[...new Array(3)].map((_, index) => (
        <EntryContainerlistItem
          key={index}
          isLoaded={false}
          container={LOADING_CONTAINER}
        />
      ))}
    </>
  );
}
