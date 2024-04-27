import { Suspense } from "solid-js";
import Layout from "../../layout";
import InfoBlock from "./components/info-block";
import AppStats from "./components/app-stats";

export default function AppDetailPage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">应用详情</h1>
      <Suspense>
        <InfoBlock />
      </Suspense>
      <Suspense>
        <AppStats />
      </Suspense>
    </Layout>
  );
}
