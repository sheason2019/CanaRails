import useSWR from "swr";
import { authClient } from "../api";

export default function useUser() {
  const { data, error, isLoading, mutate } = useSWR(["user-auth"], () =>
    authClient.getAuthData()
  );

  return {
    data: error ? undefined : data,
    isLoading,
    mutate,
  };
}
