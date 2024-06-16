import useSWRMutation from "swr/mutation";
import { entryClient } from "../../../../../api";
import useAppEntryList from "../../hooks/use-app-entry-list";
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
} from "@chakra-ui/react";

interface Props {
  isOpen: boolean;
  onClose(): void;
  entryId: number;
}

export default function EntryDeleteConfirmModal({
  isOpen,
  onClose,
  entryId,
}: Props) {
  const { mutate: mutateEntryList } = useAppEntryList();
  const { trigger: handleDelete } = useSWRMutation(
    ["delete-entry"],
    async () => entryClient.delete(entryId),
    {
      onSuccess() {
        onClose();
        mutateEntryList();
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
          <Button colorScheme="red" onClick={() => handleDelete()}>
            确认
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
}
