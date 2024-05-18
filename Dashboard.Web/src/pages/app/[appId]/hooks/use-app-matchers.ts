import { useParams } from "react-router-dom";
import useSWR from "swr";
import { appMatcherClient } from "../../../../api";

export default function useAppMatchers() {
  const { appId } = useParams();

  return useSWR(["app-matcher", appId], () =>
    appMatcherClient.list(Number(appId))
  );
}
