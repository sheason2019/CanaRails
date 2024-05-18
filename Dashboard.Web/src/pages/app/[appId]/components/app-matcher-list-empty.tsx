import { Alert, AlertIcon } from "@chakra-ui/react";

export default function AppMatcherListEmpty() {
  return (
    <Alert status="warning">
      <AlertIcon />
      暂无匹配器，该应用可能无法被正常访问
    </Alert>
  );
}
