import { useParams } from "react-router-dom";
import useSWR from "swr";
import { imageClient } from "../../../../../api";

export default function useImageList() {
  const { appId } = useParams();
  return useSWR(["image-list", appId], () => imageClient.list(Number(appId)));
}
