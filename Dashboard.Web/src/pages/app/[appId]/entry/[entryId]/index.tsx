import PageContainer from "../../../../../components/page-container";
import { Link, useParams } from "react-router-dom";
import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  Heading,
} from "@chakra-ui/react";
import useEntryDetail from "./hook/use-entry-detail";
import EntryDetailInfo from "./components/entry-detail-info";
import EntryContainerList from "./components/entry-container-list";

export default function EntryDetailPage() {
  const { appId } = useParams();
  const { data } = useEntryDetail();

  return (
    <PageContainer>
      <Heading className="my-3">{data?.name}</Heading>
      <Breadcrumb className="mb-6">
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
            流量入口详情
          </BreadcrumbLink>
        </BreadcrumbItem>
      </Breadcrumb>
      <EntryDetailInfo />
      <EntryContainerList />
    </PageContainer>
  );
}
