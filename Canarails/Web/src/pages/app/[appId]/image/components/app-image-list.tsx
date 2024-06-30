import { Alert, AlertIcon, Stack } from "@chakra-ui/react";
import useImageList from "../hooks/use-image-list";
import AppImageListLoading from "./app-image-list-loading";
import AppImageListItem from "./app-image-list-item";

export default function AppImageList() {
  const { data, isLoading } = useImageList();

  if (isLoading) {
    return <AppImageListLoading />;
  }

  if (!data?.length) {
    return (
      <Alert status="warning">
        <AlertIcon />
        暂无可用镜像
      </Alert>
    );
  }

  return (
    <Stack>
      {data.map((item) => (
        <AppImageListItem key={item.id} image={item} isLoaded />
      ))}
    </Stack>
  );
}
