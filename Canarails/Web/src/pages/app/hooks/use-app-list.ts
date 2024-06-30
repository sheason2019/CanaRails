import useSWR from "swr";
import { appClient } from "../../../api";

export default function useAppList() {
  return useSWR(["app-list"], async () => appClient.list());
}
