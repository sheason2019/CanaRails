import { useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import { entryClient } from "../../../../../../clients";

export default function useEntryContainerList() {
  const params = useParams();
  return createQuery(() => ({
    queryKey: ["container-list", params.entryID],
    queryFn: () => entryClient.listContainer(Number(params.entryID)),
    suspense: true,
    staleTime: 30_000,
  }));
}
