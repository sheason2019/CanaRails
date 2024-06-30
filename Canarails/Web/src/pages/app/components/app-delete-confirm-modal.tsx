import {
  Box,
  Button,
  Modal,
  ModalBody,
  ModalCloseButton,
  ModalContent,
  ModalFooter,
  ModalHeader,
  ModalOverlay,
  useToast,
} from "@chakra-ui/react";
import useSWRMutation from "swr/mutation";
import { appClient } from "../../../api";
import useAppList from "../hooks/use-app-list";
import { ApiException } from "../../../../api-client";

interface Props {
  isOpen: boolean;
  onClose(): void;
  appId: number;
}

export default function AppDeleteConfirmModal({
  isOpen,
  onClose,
  appId,
}: Props) {
  const toast = useToast({
    position: "bottom-right",
    variant: "left-accent",
  });
  const { mutate } = useAppList();
  const { trigger: handleDelete, isMutating } = useSWRMutation(
    ["delete-app", appId],
    () => appClient.delete(Number(appId)),
    {
      onSuccess() {
        mutate();
        onClose();
        toast({
          status: "success",
          title: "删除成功",
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
    <Modal isOpen={isOpen} onClose={onClose} isCentered>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>警告</ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <Box>数据删除后将无法被恢复，确认要执行删除操作吗？</Box>
        </ModalBody>
        <ModalFooter gap={4}>
          <Button onClick={onClose}>取消</Button>
          <Button
            colorScheme="red"
            onClick={() => handleDelete()}
            isLoading={isMutating}
          >
            确认
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
}
