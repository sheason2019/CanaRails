import { Stack } from "@chakra-ui/react";
import { ImageDTO } from "../../../../../../api-client/Image.client";
import AppImageListItem from "./app-image-list-item";

const LOADING_IMAGE: ImageDTO = {
  id: 0,
  registry: "LOADING",
  imageName: "LOADING",
  appID: 0,
  appId: 0,
  ready: false,
  createdAt: 0,
};

export default function AppImageListLoading() {
  return (
    <Stack gap={2}>
      {[...new Array(3)].map((_, index) => (
        <AppImageListItem key={index} image={LOADING_IMAGE} isLoaded={false} />
      ))}
    </Stack>
  );
}
