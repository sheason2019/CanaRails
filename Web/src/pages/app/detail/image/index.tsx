import { Suspense } from "solid-js";
import Layout from "../../../layout";
import AppImageControl from "./components/control";
import AppImageList from "./components/image-list";
import SimpleLoadingSpinner from "../../../../components/simple-loading-spinner";

export default function AppImagePage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">应用镜像</h1>
      <AppImageControl />
      <Suspense fallback={<SimpleLoadingSpinner />}>
        <AppImageList />
      </Suspense>
    </Layout>
  );
}
