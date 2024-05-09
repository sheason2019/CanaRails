import { Suspense } from "solid-js";
import Layout from "../../../layout";
import AppMatcherControl from "./components/control";
import AppMatcherList from "./components/list";
import SimpleLoadingSpinner from "../../../../components/simple-loading-spinner";

export default function AppMatcherPage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">应用匹配器</h1>
      <Suspense fallback={<SimpleLoadingSpinner />}>
        <AppMatcherControl />
        <AppMatcherList />
      </Suspense>
    </Layout>
  );
}
