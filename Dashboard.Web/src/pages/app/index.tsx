import { Heading } from "@chakra-ui/react";
import PageContainer from "../../components/page-container";
import AppControl from "./components/app-control";
import AppList from "./components/app-list";

export default function AppPage() {
  return (
    <PageContainer>
      <Heading className="my-3">应用列表</Heading>
      <AppControl />
      <AppList />
    </PageContainer>
  );
}
