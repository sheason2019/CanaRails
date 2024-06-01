import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  Heading,
} from "@chakra-ui/react";
import PageContainer from "../../../../../components/page-container";
import NewEntryForm from "./components/new-entry-form";
import { Link, useParams } from "react-router-dom";

export default function NewEntryPage() {
  const { appId } = useParams();

  return (
    <PageContainer>
      <Heading className="my-3">新建流量入口</Heading>
      <Breadcrumb className="mb-3">
        <BreadcrumbItem>
          <BreadcrumbLink as={Link} to={`/app/${appId}`}>
            应用详情
          </BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbItem>
          <BreadcrumbLink as={Link} to={`/app/${appId}/entry`}>
            流量入口
          </BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbItem isCurrentPage>
          <BreadcrumbLink as={Link} to="#">
            新建流量入口
          </BreadcrumbLink>
        </BreadcrumbItem>
      </Breadcrumb>
      <NewEntryForm />
    </PageContainer>
  );
}
