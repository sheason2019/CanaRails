import {
  Button,
  FormControl,
  FormErrorMessage,
  FormLabel,
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
import * as y from "yup";
import { imageClient } from "../../../../../api";
import useImageList from "../hooks/use-image-list";
import { useParams } from "react-router-dom";

export default function AppImageCreateButton() {
  const { appId } = useParams();
  const { mutate } = useImageList();
  const { isOpen, onOpen, onClose } = useDisclosure();
  const formik = useFormik({
    initialValues: {
      imageName: "",
    },
    validationSchema: y.object({
      imageName: y.string().required("镜像名称不能为空"),
    }),
    async onSubmit(values) {
      await imageClient.create({
        id: 0,
        registry: "",
        imageName: values.imageName,
        appId: Number(appId),
        ready: false,
        createdAt: 0
      });
      mutate();
      onClose();
    },
  });

  return (
    <>
      <Button width={28} colorScheme="blue" onClick={onOpen}>
        新建
      </Button>
      <Modal isOpen={isOpen} onClose={onClose} isCentered>
        <ModalOverlay />
        <ModalContent>
          <form onSubmit={formik.handleSubmit}>
            <ModalHeader>新建镜像</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              <FormControl isInvalid={!!formik.errors.imageName}>
                <FormLabel htmlFor="create-image__imageName">
                  镜像名称
                </FormLabel>
                <Input
                  id="create-image__imageName"
                  name="imageName"
                  type="text"
                  value={formik.values.imageName}
                  onChange={formik.handleChange}
                  readOnly={formik.isSubmitting}
                />
                <FormErrorMessage>{formik.errors.imageName}</FormErrorMessage>
              </FormControl>
            </ModalBody>
            <ModalFooter gap={2}>
              <Button type="reset" onClick={onClose}>
                取消
              </Button>
              <Button type="submit" colorScheme="blue">
                提交
              </Button>
            </ModalFooter>
          </form>
        </ModalContent>
      </Modal>
    </>
  );
}
