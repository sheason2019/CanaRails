import { Breadcrumb, BreadcrumbItem, BreadcrumbLink } from "@chakra-ui/react";
import PageContainer from "../../../components/page-container";
import AppDetailInfo from "./components/app-detail-info";
import AppHostnames from "./components/app-hostnames";
import AppStats from "./components/app-stats";
import AppHeading from "./components/app-heading";

export default function AppDetailPage() {
  return (
    <PageContainer>
      <AppHeading />
      <Breadcrumb>
        <BreadcrumbItem isCurrentPage>
          <BreadcrumbLink>应用详情</BreadcrumbLink>
        </BreadcrumbItem>
      </Breadcrumb>
      <AppDetailInfo />
      <AppStats />
      <AppHostnames />
    </PageContainer>
  );
}
