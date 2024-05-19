import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  Heading,
} from "@chakra-ui/react";
import PageContainer from "../../../../components/page-container";
import { Link, useParams } from "react-router-dom";
import AppImageControl from "./components/app-image-control";
import AppImageList from "./components/app-image-list";

export default function AppImagePage() {
  const { appId } = useParams();
  return (
    <PageContainer>
      <Heading className="my-3">可用镜像</Heading>
      <Breadcrumb className="mb-3">
        <BreadcrumbItem>
          <BreadcrumbLink as={Link} to={`/app/${appId}`}>
            应用详情
          </BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbItem isCurrentPage>
          <BreadcrumbLink>可用镜像</BreadcrumbLink>
        </BreadcrumbItem>
      </Breadcrumb>
      <AppImageControl />
      <AppImageList />
    </PageContainer>
  );
}
