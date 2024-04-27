import { useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import { appClient } from "../../../../clients";

export default function useApp() {
  const params = useParams();

  return createQuery(() => ({
    queryKey: ["query-app", params.id],
    queryFn: async () => appClient.findByID(Number(params.id)),
    suspense: true,
  }));
}
