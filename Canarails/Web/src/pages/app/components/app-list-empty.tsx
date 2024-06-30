import { Button, Stack } from "@chakra-ui/react";
import { Link } from "react-router-dom";

export default function AppListEmpty() {
  return (
    <Stack height={48} alignItems="center" justifyContent="center">
      <p className="mb-6 text-sm text-gray-500">应用列表暂无内容，不如</p>
      <Link to="/app/new">
        <Button colorScheme="blue" paddingX={12}>
          新建一个应用
        </Button>
      </Link>
    </Stack>
  );
}
