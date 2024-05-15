import PageContainer from "../../components/page-container";
import { Alert, AlertIcon } from "@chakra-ui/react";

export default function DashboardPage() {
  return (
    <PageContainer>
      <Alert status="warning">
        <AlertIcon />
        <span>仪表盘尚未完成，先去别处看看吧</span>
      </Alert>
    </PageContainer>
  );
}
