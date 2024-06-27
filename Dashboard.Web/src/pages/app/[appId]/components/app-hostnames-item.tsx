import {
  IconButton,
  ListItem,
  Menu,
  MenuButton,
  MenuItem,
  MenuList,
  useToast,
} from "@chakra-ui/react";
import { DeleteIcon, HamburgerIcon } from "@chakra-ui/icons";
import { appClient } from "../../../../api";
import useAppDetail from "../hooks/use-app-detail";
import { ApiException } from "../../../../../api-client";

interface Props {
  hostname: string;
}

export default function AppHostnamesItem({ hostname }: Props) {
  const toast = useToast({
    position: "bottom-right",
    variant: "left-accent",
  });
  const { data, mutate } = useAppDetail();

  const handleDelete = async () => {
    try {
      await appClient.deleteHostname(data?.id ?? 0, hostname);
      mutate();
      toast({
        status: "success",
        title: "删除成功",
      });
    } catch (e) {
      const isApiException = e instanceof ApiException;
      toast({
        status: "error",
        title: isApiException ? "请求失败" : "未知错误",
        description: isApiException ? e.response : String(e),
      });
    }
  };

  return (
    <ListItem className="mb-3 flex items-center gap-2">
      <Menu>
        <MenuButton
          as={IconButton}
          icon={<HamburgerIcon />}
          aria-label="open app matcher menu"
          variant="ghost"
        />
        <MenuList>
          <MenuItem color="red" icon={<DeleteIcon />} onClick={handleDelete}>
            删除
          </MenuItem>
        </MenuList>
      </Menu>
      {hostname}
    </ListItem>
  );
}
