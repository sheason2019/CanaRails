import { useParams } from "react-router-dom";
import useSWR from "swr";
import { containerClient } from "../../../../../../api";

export default function useContainerList() {
  const { entryId } = useParams();
  return useSWR(["container-list", entryId], async () =>
    containerClient.list(Number(entryId))
  );
}
