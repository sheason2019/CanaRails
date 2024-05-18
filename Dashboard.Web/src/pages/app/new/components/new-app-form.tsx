import {
  Button,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Input,
  Textarea,
  VStack,
} from "@chakra-ui/react";
import { useFormik } from "formik";
import { useNavigate } from "react-router-dom";
import * as y from "yup";
import { appClient } from "../../../../api";

export default function NewAppForm() {
  const navigate = useNavigate();

  const formik = useFormik({
    initialValues: {
      name: "",
      description: "",
    },
    validationSchema: y.object({
      name: y.string().required("App名称不能为空"),
    }),
    async onSubmit(values) {
      const app = await appClient.create({
        id: 0,
        name: values.name,
        description: values.description,
      });
      navigate(`/app/${app.id}`);
    },
  });

  return (
    <form onSubmit={formik.handleSubmit}>
      <VStack spacing={4} alignItems="flex-start">
        <FormControl className="mt-6 max-w-sm" isInvalid={!!formik.errors.name}>
          <FormLabel htmlFor="name">App 名称</FormLabel>
          <Input
            id="name"
            name="name"
            readOnly={formik.isSubmitting}
            type="text"
            onChange={formik.handleChange}
            value={formik.values.name}
          />
          <FormErrorMessage>{formik.errors.name}</FormErrorMessage>
        </FormControl>
        <FormControl>
          <FormLabel htmlFor="description">App 简介</FormLabel>
          <Textarea
            id="description"
            name="description"
            readOnly={formik.isSubmitting}
            placeholder="选填"
            onChange={formik.handleChange}
            value={formik.values.description}
          />
        </FormControl>
        <Button
          type="submit"
          colorScheme="blue"
          isLoading={formik.isSubmitting}
        >
          新建应用
        </Button>
      </VStack>
    </form>
  );
}
