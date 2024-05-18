import { Breadcrumb, BreadcrumbItem, BreadcrumbLink } from "@chakra-ui/react";
import PageContainer from "../../../../components/page-container";
import AppHeading from "../components/app-heading";
import { Link, useParams } from "react-router-dom";
import EntryList from "./components/entry-list";
import EntryControl from "./components/entry-control";

export default function AppEntryPage() {
  const { appId } = useParams();

  return (
    <PageContainer>
      <AppHeading />
      <Breadcrumb className="mb-3">
        <BreadcrumbItem>
          <BreadcrumbLink as={Link} to={`/app/${appId}`}>
            应用详情
          </BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbItem isCurrentPage>
          <BreadcrumbLink>流量入口</BreadcrumbLink>
        </BreadcrumbItem>
      </Breadcrumb>
      <EntryControl />
      <EntryList />
    </PageContainer>
  );
}
