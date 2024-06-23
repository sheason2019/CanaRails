import {
  Avatar,
  Box,
  Button,
  CardBody,
  CardHeader,
  Heading,
  Stack,
} from "@chakra-ui/react";
import useUser from "../../../hooks/use-user";
import useSWRMutation from "swr/mutation";
import { authClient } from "../../../api";

export default function LoginState() {
  const { data, mutate } = useUser();

  const { trigger } = useSWRMutation(["logout"], () => authClient.logout(), {
    onSuccess() {
      mutate();
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
