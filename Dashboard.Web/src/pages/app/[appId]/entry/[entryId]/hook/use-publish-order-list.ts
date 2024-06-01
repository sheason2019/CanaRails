import { useParams } from "react-router-dom";
import useSWR from "swr";
import { publishOrderClient } from "../../../../../../api";

export default function usePublishOrderList() {
  const { entryId } = useParams();
  return useSWR(["publish-order-list", entryId], async () =>
    publishOrderClient.list(Number(entryId))
  );
}
