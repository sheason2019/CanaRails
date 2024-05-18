import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  Heading,
  Skeleton,
} from "@chakra-ui/react";
import PageContainer from "../../../components/page-container";
import useAppDetail from "./hooks/use-app-detail";
import AppDetailInfo from "./components/app-detail-info";
import AppMatcherList from "./components/app-matcher-list";
import AppStats from "./components/app-stats";

export default function AppDetailPage() {
  const { data, isLoading } = useAppDetail();

  return (
    <PageContainer>
      <Skeleton isLoaded={!isLoading}>
        <Heading className="my-3">{data?.name}</Heading>
      </Skeleton>
      <Breadcrumb>
        <BreadcrumbItem isCurrentPage>
          <BreadcrumbLink href="#">应用详情</BreadcrumbLink>
        </BreadcrumbItem>
      </Breadcrumb>
      <AppDetailInfo />
      <AppStats />
      <AppMatcherList />
    </PageContainer>
  );
}
