import {
  Button,
  Flex,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Input,
  Textarea,
  VStack,
  useToast,
} from "@chakra-ui/react";
import { useFormik } from "formik";
import * as y from "yup";
import { entryClient } from "../../../../../../api";
import { useNavigate, useParams } from "react-router-dom";
import { ApiException } from "../../../../../../../api-client";

export default function NewEntryForm() {
  const toast = useToast({
    position: "bottom-right",
    variant: "left-accent",
  });
  const { appId } = useParams();
  const navigate = useNavigate();

  const formik = useFormik({
    initialValues: {
      name: "",
      description: "",
    },
    validationSchema: y.object({
      name: y.string().required("流量入口名称不能为空"),
    }),
    async onSubmit(values) {
      try {
        const resp = await entryClient.create({
          ...values,
          id: 0,
          deployedAt: 0,
          matchers: [],
          appId: Number(appId),
        });
        navigate(`/app/${appId}/entry/${resp.id}`);
        toast({
          status: "success",
          title: "新建流量入口成功",
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

  return (
    <form onSubmit={formik.handleSubmit}>
      <VStack gap={4} alignItems="flex-start">
        <FormControl className="max-w-sm" isInvalid={!!formik.errors.name}>
          <FormLabel htmlFor="new-entry__name">流量入口名称</FormLabel>
          <Input
            id="new-entry__name"
            name="name"
            value={formik.values.name}
            onChange={formik.handleChange}
            readOnly={formik.isSubmitting}
          />
          <FormErrorMessage>{formik.errors.name}</FormErrorMessage>
        </FormControl>
        <FormControl isInvalid={!!formik.errors.description}>
          <FormLabel htmlFor="new-entry__description">简介</FormLabel>
          <Textarea
            id="new-entry__description"
            name="description"
            value={formik.values.description}
            onChange={formik.handleChange}
            readOnly={formik.isSubmitting}
          />
          <FormErrorMessage>{formik.errors.description}</FormErrorMessage>
        </FormControl>
        <Flex gap={4}>
          <Button type="submit" colorScheme="blue">
            提交
          </Button>
          <Button type="reset" onClick={formik.handleReset}>
            重置
          </Button>
        </Flex>
      </VStack>
    </form>
  );
}
