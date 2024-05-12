import { useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import { entryClient } from "../../../../../clients";

export default function useEntryMatcherList() {
  const params = useParams();
  
  return createQuery(() => ({
    queryKey: ["entry-matcher-list", params.entryID],
    queryFn: () => entryClient.listMatcher(Number(params.entryID)),
    suspense: true,
    staleTime: 30_000,
  }));
}
