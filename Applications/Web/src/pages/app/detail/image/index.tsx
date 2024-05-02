import { Suspense } from "solid-js";
import Layout from "../../../layout";
import AppImageControl from "./components/control";
import AppImageList from "./components/image-list";
import RecordListLoadingSpinner from "../../../../components/record-list-loading-spinner";

export default function AppImagePage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">应用镜像</h1>
      <AppImageControl />
      <Suspense fallback={<RecordListLoadingSpinner />}>
        <AppImageList />
      </Suspense>
    </Layout>
  );
}
