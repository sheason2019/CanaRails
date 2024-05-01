import Layout from "../../../layout";
import AppImageControl from "./components/control";

export default function AppImagePage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">应用镜像</h1>
      <AppImageControl />
    </Layout>
  );
}
