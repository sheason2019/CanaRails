import { Card } from "@chakra-ui/react";
import useUser from "../../hooks/use-user";
import LoginForm from "./components/login-form";
import LoginState from "./components/login-state";

export default function LoginPage() {
  const { data } = useUser();

  return (
    <div className="flex-1 flex items-center justify-center px-4">
      <Card className="w-96">{data ? <LoginState /> : <LoginForm />}</Card>
    </div>
  );
}
