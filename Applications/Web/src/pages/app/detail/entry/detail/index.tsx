import { Suspense } from "solid-js";
import Layout from "../../../../layout";
import ContainerList from "./components/container-list";
import AppEntryInfo from "./components/entry-info";
import SimpleLoadingSpinner from "../../../../../components/simple-loading-spinner";

export default function AppEntryDetailPage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">入口详情</h1>
      <Suspense fallback={<SimpleLoadingSpinner />}>
        <AppEntryInfo />
      </Suspense>
      <Suspense>
        <ContainerList />
      </Suspense>
    </Layout>
  );
}
