import useSWR from "swr";
import { authClient } from "../api";
import { Button, Skeleton } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";

export default function UserButton() {
  const navigate = useNavigate();
  const { data, isLoading } = useSWR(["auth-data"], authClient.getAuthData);

  if (!data) {
    return (
      <Skeleton isLoaded={!isLoading}>
        <Button onClick={() => navigate("/login")}>LOGIN</Button>
      </Skeleton>
    );
  }

  return <Button>{data?.username}</Button>;
}
