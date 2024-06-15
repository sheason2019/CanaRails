import {
  Card,
  Skeleton,
  CardBody,
  Flex,
  Text,
  Menu,
  MenuButton,
  IconButton,
  MenuList,
  MenuItem,
  useDisclosure,
} from "@chakra-ui/react";
import { AppDTO } from "../../../../api-client/App.client";
import { Link } from "react-router-dom";
import { HamburgerIcon } from "@chakra-ui/icons";
import AppDeleteConfirmModal from "./app-delete-confirm-modal";

interface Props {
  app: AppDTO;
  isLoaded: boolean;
}

export default function AppListItem({ app, isLoaded }: Props) {
  const { isOpen, onClose, onOpen } = useDisclosure();

  return (
    <>
      <Link to={isLoaded ? `/app/${app.id}` : "#"}>
        <Card>
          <Skeleton isLoaded={isLoaded}>
            <CardBody>
              <Flex alignItems="center">
                <Flex gap={2} alignItems="baseline" flexGrow={1}>
                  <p className="text-lg font-bold">{app.name}</p>
                  <p className="text-gray-500 font-bold text-sm">ID {app.id}</p>
                </Flex>
                <object
                  onClick={(e) => {
                    e.preventDefault();
                    e.stopPropagation();
                  }}
                >
                  <Menu>
                    <MenuButton
                      as={IconButton}
                      icon={<HamburgerIcon />}
                      variant="ghost"
                    />
                    <MenuList>
                      <MenuItem color="red" onClick={onOpen}>
                        删除
                      </MenuItem>
                    </MenuList>
                  </Menu>
                </object>
              </Flex>
              <Text className="mt-1">{app.description}</Text>
            </CardBody>
          </Skeleton>
        </Card>
      </Link>
      <AppDeleteConfirmModal appId={app.id} isOpen={isOpen} onClose={onClose} />
    </>
  );
}
