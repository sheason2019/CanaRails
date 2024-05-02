import { Suspense } from "solid-js";
import Layout from "../layout";
import AppPageControl from "./components/control";
import AppList from "./components/list";
import RecordListLoadingSpinner from "../../components/record-list-loading-spinner";

export default function AppPage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">应用列表</h1>
      <AppPageControl />
      <Suspense fallback={<RecordListLoadingSpinner />}>
        <AppList />
      </Suspense>
    </Layout>
  );
}
