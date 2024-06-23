import { Button, Skeleton } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import useUser from "../hooks/use-user";

export default function UserButton() {
  const navigate = useNavigate();
  const { data, isLoading } = useUser();

  return (
    <Skeleton isLoaded={!isLoading}>
      <Button onClick={() => navigate("/login")} variant="ghost">
        {data?.username ?? "LOGIN"}
      </Button>
    </Skeleton>
  );
}
