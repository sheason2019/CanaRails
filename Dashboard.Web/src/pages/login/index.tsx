import {
  Button,
  Card,
  CardBody,
  CardHeader,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Heading,
  Input,
  Stack,
} from "@chakra-ui/react";
import { useFormik } from "formik";
import * as y from "yup";
import { authClient } from "../../api";

export default function LoginPage() {
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
      const resp = await authClient.login(values);
    },
  });

  return (
    <div className="flex-1 flex items-center justify-center px-4">
      <Card className="w-96">
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
      </Card>
    </div>
  );
}
