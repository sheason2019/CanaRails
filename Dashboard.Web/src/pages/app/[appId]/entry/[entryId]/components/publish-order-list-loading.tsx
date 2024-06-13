import { PublishOrderDTO } from "../../../../../../../api-client/PublishOrder.client";
import PublishOrderListItem from "./publish-order-list-item";

const LOADING_CONTAINER: PublishOrderDTO = {
  id: 0,
  imageId: 0,
  entryId: 0,
  version: 0,
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
