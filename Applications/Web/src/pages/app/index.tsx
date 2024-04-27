import { Suspense } from "solid-js";
import Layout from "../layout";
import AppPageControl from "./components/control";
import AppList from "./components/list";

export default function AppPage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">应用列表</h1>
      <AppPageControl />
      <Suspense
        fallback={
          <div class="text-center">
            <span class="loading loading-spinner loading-lg h-24"></span>
          </div>
        }
      >
        <AppList />
      </Suspense>
    </Layout>
  );
}
