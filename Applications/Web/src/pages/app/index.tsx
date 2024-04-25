import Layout from "../layout";
import AppPageControl from "./components/control";

export default function AppPage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">应用列表</h1>
      <AppPageControl />
    </Layout>
  );
}
