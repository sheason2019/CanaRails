import {
  Button,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Input,
  Modal,
  ModalBody,
  ModalContent,
  ModalFooter,
  ModalHeader,
  ModalOverlay,
  Stack,
  useDisclosure,
  useToast,
} from "@chakra-ui/react";
import { useFormik } from "formik";
import * as y from "yup";
import { useParams } from "react-router-dom";
import { entryClient } from "../../../../../../api";
import { ApiException } from "../../../../../../../api-client";
import useEntryDetail from "../hook/use-entry-detail";
import { useEffect } from "react";

export default function EntryMatcherCreateButton() {
  const toast = useToast({
    position: "bottom-right",
    variant: "left-accent",
  });

  const { entryId } = useParams();
  const { mutate } = useEntryDetail();
  const { isOpen, onClose, onOpen } = useDisclosure();
  const formik = useFormik({
    initialValues: {
      key: "",
      value: "",
    },
    validationSchema: y.object({
      key: y.string().required("Key 值不能为空"),
      value: y.string().required("Value 值不能为空"),
    }),
    async onSubmit(values) {
      try {
        await entryClient.createMatcher(Number(entryId), {
          key: values.key,
          value: values.value,
        });
        mutate();
        onClose();
        toast({
          status: "success",
          title: "创建成功",
        });
      } catch (e) {
        const isApiException = e instanceof ApiException;
        toast({
          status: "error",
          title: isApiException ? "请求失败" : "未知错误",
          description: isApiException ? e.response : String(e),
        });
      }
    },
  });

  useEffect(() => {
    formik.resetForm();
  }, [isOpen]);

  return (
    <>
      <Button onClick={onOpen}>创建</Button>
      <Modal isOpen={isOpen} onClose={onClose} isCentered>
        <ModalOverlay />
        <ModalContent>
          <form onSubmit={formik.handleSubmit}>
            <ModalHeader>创建流量入口匹配器</ModalHeader>
            <ModalBody as={Stack} gap={4}>
              <FormControl isInvalid={!!formik.errors.key}>
                <FormLabel htmlFor="create-entry-matcher__key">Key</FormLabel>
                <Input
                  id="create-entry-matcher__key"
                  name="key"
                  value={formik.values.key}
                  onChange={formik.handleChange}
                  readOnly={formik.isSubmitting}
                />
                <FormErrorMessage>{formik.errors.key}</FormErrorMessage>
              </FormControl>
              <FormControl isInvalid={!!formik.errors.value}>
                <FormLabel htmlFor="create-entry-matcher__value">
                  Value
                </FormLabel>
                <Input
                  id="create-entry-matcher__value"
                  name="value"
                  value={formik.values.value}
                  onChange={formik.handleChange}
                  readOnly={formik.isSubmitting}
                />
                <FormErrorMessage>{formik.errors.value}</FormErrorMessage>
              </FormControl>
            </ModalBody>
            <ModalFooter gap={2}>
              <Button isDisabled={formik.isSubmitting} onClick={onClose}>
                取消
              </Button>
              <Button
                isDisabled={formik.isSubmitting}
                colorScheme="blue"
                type="submit"
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
