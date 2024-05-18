import { useParams } from "react-router-dom";
import useSWR from "swr";
import { entryClient } from "../../../../../../api";

export default function useEntryDetail() {
  const { entryId } = useParams();
  return useSWR(["entry-detail", entryId], () =>
    entryClient.findByID(Number(entryId))
  );
}
