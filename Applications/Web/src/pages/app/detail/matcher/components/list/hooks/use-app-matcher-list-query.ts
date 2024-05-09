import { useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import { appMatcherClient } from "../../../../../../../clients";

export default function useAppMatcherListQuery() {
  const params = useParams();

  const query = createQuery(() => ({
    queryKey: ["app-matcher-list", params.id],
    queryFn: async () => appMatcherClient.list(Number(params.id)),
    suspense: true,
  }));

  return query;
}
