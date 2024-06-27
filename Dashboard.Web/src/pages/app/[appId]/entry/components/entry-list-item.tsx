import {
  Card,
  CardBody,
  Flex,
  IconButton,
  Menu,
  MenuButton,
  MenuItem,
  MenuList,
  Skeleton,
  Tag,
  Text,
  useDisclosure,
  useToast,
} from "@chakra-ui/react";
import { ApiException, EntryDTO } from "../../../../../../api-client";
import { Link } from "react-router-dom";
import { HamburgerIcon } from "@chakra-ui/icons";
import useAppDetail from "../../hooks/use-app-detail";
import useSWRMutation from "swr/mutation";
import { appClient } from "../../../../../api";
import EntryDeleteConfirmModal from "./entry-delete-confirm-modal";

interface Props {
  entry: EntryDTO;
  isDefault: boolean;
  isLoaded: boolean;
}

export default function EntryListItem({ entry, isDefault, isLoaded }: Props) {
  const toast = useToast({
    position: "bottom-right",
    variant: "left-accent",
  });
  const { isOpen, onClose, onOpen } = useDisclosure();
  const { data, mutate: mutateAppDetail } = useAppDetail();

  const { trigger: handleSetDefaultEntry } = useSWRMutation(
    ["put-default-entry"],
    async () => appClient.putDefaultEntry(data!.id, entry.id),
    {
      onSuccess() {
        mutateAppDetail();
        toast({
          status: "success",
          title: "修改默认流量入口成功",
        });
      },
      onError(err) {
        const isApiException = err instanceof ApiException;
        toast({
          status: "error",
          title: isApiException ? "请求失败" : "未知错误",
          description: isApiException ? err.response : String(err),
        });
      },
    }
  );

  return (
    <>
      <Card
        as={Link}
        to={`/app/${entry.appId}/entry/${entry.id}`}
        _hover={{ shadow: "lg" }}
        className="transition-shadow"
      >
        <Skeleton isLoaded={isLoaded}>
          <CardBody>
            <Flex gap={2} alignItems="center" className="mb-2">
              {isDefault && <Tag colorScheme="blue">默认入口</Tag>}
              <Flex alignItems="baseline" gap={2} flexGrow={1}>
                <p className="text-lg font-bold">{entry.name}</p>
                <p className="text-gray-500 font-bold text-sm">ID {entry.id}</p>
              </Flex>
              <object
                onClick={(e) => {
                  e.stopPropagation();
                  e.preventDefault();
                }}
              >
                <Menu>
                  <MenuButton
                    as={IconButton}
                    aria-label="entry-menu"
                    icon={<HamburgerIcon />}
                    variant="ghost"
                  />
                  <MenuList>
                    <MenuItem onClick={() => handleSetDefaultEntry()}>
                      设置为默认入口
                    </MenuItem>
                    <MenuItem color="red" onClick={onOpen}>
                      删除
                    </MenuItem>
                  </MenuList>
                </Menu>
              </object>
            </Flex>
            <Text>{entry.description}</Text>
          </CardBody>
        </Skeleton>
      </Card>
      <EntryDeleteConfirmModal
        isOpen={isOpen}
        entryId={entry.id}
        onClose={onClose}
      />
    </>
  );
}
