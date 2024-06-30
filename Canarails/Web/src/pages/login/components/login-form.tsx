import {
  Stack,
  FormControl,
  FormLabel,
  Input,
  FormErrorMessage,
  Button,
  CardBody,
  CardHeader,
  Heading,
  useToast,
} from "@chakra-ui/react";
import { useFormik } from "formik";
import * as y from "yup";
import { authClient } from "../../../api";
import useUser from "../../../hooks/use-user";
import { ApiException } from "../../../../api-client";

export default function LoginForm() {
  const toast = useToast({
    position: "bottom-right",
    variant: "left-accent",
  });
  const { mutate } = useUser();

  const formik = useFormik({
    initialValues: {
      username: "",
      password: "",
    },
    validationSchema: y.object({
      username: y.string().required(),
      password: y.string().required(),
    }),
    async onSubmit(values) {
      try {
        await authClient.login(values);
        mutate();
        toast({
          status: "success",
          title: "登录成功",
        });
      } catch (e) {
        if (e instanceof ApiException) {
          toast({
            status: "error",
            title: "登录失败",
            description: e.response,
          });
        } else {
          toast({
            status: "error",
            title: "未知错误",
            description: String(e),
          });
        }
      }
    },
  });

  return (
    <>
      <CardHeader>
        <Heading size="md">用户登录</Heading>
      </CardHeader>
      <CardBody>
        <form onSubmit={formik.handleSubmit}>
          <Stack spacing={4}>
            <FormControl
              className="max-w-sm"
              isInvalid={!!formik.errors.username}
            >
              <FormLabel htmlFor="login_username">用户名</FormLabel>
              <Input
                id="login_username"
                name="username"
                readOnly={formik.isSubmitting}
                type="text"
                onChange={formik.handleChange}
                value={formik.values.username}
              />
              <FormErrorMessage>{formik.errors.username}</FormErrorMessage>
            </FormControl>
            <FormControl>
              <FormLabel htmlFor="login_password">密码</FormLabel>
              <Input
                id="login_password"
                name="password"
                type="password"
                readOnly={formik.isSubmitting}
                onChange={formik.handleChange}
                value={formik.values.password}
              />
            </FormControl>
            <Button
              type="submit"
              colorScheme="blue"
              className="mt-6"
              isLoading={formik.isSubmitting}
            >
              登录
            </Button>
          </Stack>
        </form>
      </CardBody>
    </>
  );
}
