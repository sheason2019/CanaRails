import { EntryVersionDTO } from "../../../../../../../api-client";
import PublishOrderListItem from "./publish-order-list-item";

const LOADING_CONTAINER: EntryVersionDTO = {
  id: 0,
  imageId: 0,
  entryId: 0,
  port: 0,
  replica: 0
};

export default function PublishOrderListLoading() {
  return (
    <>
      {[...new Array(3)].map((_, index) => (
        <PublishOrderListItem
          key={index}
          isLoaded={false}
          publishOrder={LOADING_CONTAINER}
        />
      ))}
    </>
  );
}
