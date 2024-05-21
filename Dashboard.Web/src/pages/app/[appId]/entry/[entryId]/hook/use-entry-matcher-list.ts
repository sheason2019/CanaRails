import { useParams } from "react-router-dom";
import useSWR from "swr";
import { entryMatcherClient } from "../../../../../../api";

export default function useEntryMatcherList() {
  const { entryId } = useParams();

  return useSWR(["entry-matcher-list", entryId], () =>
    entryMatcherClient.list(Number(entryId))
  );
}
