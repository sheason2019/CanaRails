import {
  Avatar,
  Box,
  Button,
  CardBody,
  CardHeader,
  Heading,
  Stack,
  useToast,
} from "@chakra-ui/react";
import useUser from "../../../hooks/use-user";
import useSWRMutation from "swr/mutation";
import { authClient } from "../../../api";
import { ApiException } from "../../../../api-client";

export default function LoginState() {
  const toast = useToast({
    position: "bottom-right",
    variant: "left-accent",
  });

  const { data, mutate } = useUser();

  const { trigger } = useSWRMutation(["logout"], () => authClient.logout(), {
    onSuccess() {
      mutate();
      toast({
        status: "success",
        title: "退出登录成功",
      });
    },
    onError(err) {
      if (err instanceof ApiException) {
        toast({
          status: "error",
          title: "请求失败",
          description: err.response,
        });
      } else {
        toast({
          status: "error",
          title: "未知错误",
          description: String(err),
        });
      }
    },
  });

  return (
    <>
      <CardHeader>
        <Heading size="md">用户信息</Heading>
      </CardHeader>
      <CardBody>
        <Stack>
          <Stack alignItems="center">
            <Avatar />
            <Box fontWeight="bold" textAlign="center">
              {data?.username}
            </Box>
          </Stack>
          <Button mt={4} colorScheme="red" onClick={() => trigger()}>
            退出登录
          </Button>
        </Stack>
      </CardBody>
    </>
  );
}
