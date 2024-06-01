import { useParams } from "react-router-dom";
import useSWR from "swr";
import { appClient } from "../../../../api";

export default function useAppDetail() {
  const { appId } = useParams();
  return useSWR(["app-detail", appId], async () =>
    appClient.findByID(Number(appId))
  );
}
