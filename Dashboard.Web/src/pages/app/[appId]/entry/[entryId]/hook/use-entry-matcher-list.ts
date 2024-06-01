import { useParams } from "react-router-dom";
import useSWR from "swr";

export default function useEntryMatcherList() {
  const { entryId } = useParams();

  return useSWR(["entry-matcher-list", entryId], () => []);
}
