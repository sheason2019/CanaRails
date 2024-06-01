import { AddIcon } from "@chakra-ui/icons";
import {
  Button,
  FormControl,
  FormErrorMessage,
  IconButton,
  Input,
  Modal,
  ModalBody,
  ModalCloseButton,
  ModalContent,
  ModalFooter,
  ModalHeader,
  ModalOverlay,
  useDisclosure,
} from "@chakra-ui/react";
import { useFormik } from "formik";
import { useEffect } from "react";
import { useParams } from "react-router-dom";
import * as y from "yup";
import useAppDetail from "../hooks/use-app-detail";
import { appClient } from "../../../../api";

export default function AppMatcherCreateButton() {
  const { appId } = useParams();
  const { isOpen, onOpen, onClose } = useDisclosure();
  const { mutate } = useAppDetail();

  const formik = useFormik({
    initialValues: {
      host: "",
    },
    validationSchema: y.object({
      host: y.string().required("Host不能为空"),
    }),
    async onSubmit(values) {
      await appClient.createHostname(Number(appId), { hostname: values.host });
      onClose();
      mutate();
    },
  });
  const { resetForm } = formik;

  useEffect(() => {
    resetForm();
  }, [isOpen, resetForm]);

  return (
    <>
      <IconButton aria-label="create app matcher" onClick={onOpen}>
        <AddIcon />
      </IconButton>
      <Modal isOpen={isOpen} onClose={onClose} isCentered>
        <ModalOverlay />
        <ModalContent>
          <form onSubmit={formik.handleSubmit}>
            <ModalHeader>创建 Hostname</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              <FormControl isInvalid={!!formik.errors.host}>
                <Input
                  name="host"
                  id="create-matcher__host"
                  readOnly={formik.isSubmitting}
                  type="text"
                  placeholder="请输入 Hostname"
                  value={formik.values.host}
                  onChange={formik.handleChange}
                />
                <FormErrorMessage>{formik.errors.host}</FormErrorMessage>
              </FormControl>
            </ModalBody>
            <ModalFooter gap={3}>
              <Button type="reset" isDisabled={formik.isSubmitting}>
                取消
              </Button>
              <Button
                type="submit"
                isLoading={formik.isSubmitting}
                colorScheme="blue"
              >
                提交
              </Button>
            </ModalFooter>
          </form>
        </ModalContent>
      </Modal>
    </>
  );
}
