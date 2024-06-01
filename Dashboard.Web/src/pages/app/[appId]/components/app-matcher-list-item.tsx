import {
  IconButton,
  ListItem,
  Menu,
  MenuButton,
  MenuItem,
  MenuList,
} from "@chakra-ui/react";
import { DeleteIcon, HamburgerIcon } from "@chakra-ui/icons";
import { appClient } from "../../../../api";
import useAppDetail from "../hooks/use-app-detail";

interface Props {
  hostname: string;
}

export default function AppMatcherListItem({ hostname }: Props) {
  const { data, mutate } = useAppDetail();

  const handleDelete = async () => {
    await appClient.deleteHostname(data?.id ?? 0, hostname);
    mutate();
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
