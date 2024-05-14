import { useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import { entryClient } from "../../../../../clients";

export default function useEntryList() {
  const params = useParams();

  return createQuery(() => ({
    queryKey: ["query-entry-list", params.id],
    queryFn: () => entryClient.list(Number(params.id)),
    suspense: true,
    staleTime: 30_000,
  }));
}
