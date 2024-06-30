import { Alert, AlertIcon } from "@chakra-ui/react";

export default function AppHostnamesEmpty() {
  return (
    <Alert status="warning">
      <AlertIcon />
      暂未声明 Hostname ，该应用可能无法被正常访问
    </Alert>
  );
}
