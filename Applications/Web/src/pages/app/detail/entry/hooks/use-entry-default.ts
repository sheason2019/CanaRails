import { useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import { entryClient } from "../../../../../clients";

export default function useEntryDefault() {
  const params = useParams();

  return createQuery(() => ({
    queryKey: ["entry-default", params.id],
    queryFn: () => entryClient.findDefaultEntry(Number(params.id)),
    suspense: true,
    staleTime: 180_000,
  }));
}
