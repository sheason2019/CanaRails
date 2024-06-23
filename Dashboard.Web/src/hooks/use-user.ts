import useSWR from "swr";
import { authClient } from "../api";
import { ApiException } from "../../api-client";

export default function useUser() {
  const { data, error, isLoading, mutate } = useSWR(
    ["user-auth"],
    () => authClient.getAuthData(),
    {
      onErrorRetry(err) {
        if (err instanceof ApiException) {
          if (err.status === 401) {
            return;
          }
        }
      },
    }
  );

  return {
    data: error ? undefined : data,
    isLoading,
    mutate,
  };
}
