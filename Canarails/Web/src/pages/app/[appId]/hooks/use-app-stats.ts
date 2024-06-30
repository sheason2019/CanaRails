import { useParams } from "react-router-dom";
import useSWR from "swr";
import { entryClient, imageClient } from "../../../../api";

export default function useAppStats() {
  const { appId } = useParams();

  return useSWR(["app-stats", appId], async () => {
    const id = Number(appId);
    const [entryResp, imageResp] = await Promise.all([
      entryClient.count(id),
      imageClient.count(id),
    ]);

    return {
      entry: entryResp,
      image: imageResp,
    };
  });
}
