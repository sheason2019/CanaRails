import {
  IconButton,
  ListItem,
  Menu,
  MenuButton,
  MenuItem,
  MenuList,
} from "@chakra-ui/react";
import { AppMatcherDTO } from "../../../../../api-client/AppMatcher.client";
import { DeleteIcon, HamburgerIcon } from "@chakra-ui/icons";
import { appMatcherClient } from "../../../../api";
import useAppMatchers from "../hooks/use-app-matchers";

interface Props {
  appMatcher: AppMatcherDTO;
}

export default function AppMatcherListItem({ appMatcher }: Props) {
  const { mutate } = useAppMatchers();

  const handleDelete = async () => {
    await appMatcherClient.delete(appMatcher.appID, appMatcher.id);
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
      {appMatcher.host}
    </ListItem>
  );
}
