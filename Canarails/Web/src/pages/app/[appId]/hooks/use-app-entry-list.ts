import { useParams } from "react-router-dom";
import { entryClient } from "../../../../api";
import useSWR from "swr";

export default function useAppEntryList() {
  const { appId } = useParams();
  return useSWR(["entry-list", appId], () => entryClient.list(Number(appId)));
}
