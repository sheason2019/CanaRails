import Layout from "../../../layout";
import AppInstanceControl from "./components/control";
import DataTable from "./components/data-table";

export default function AppInstancePage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">应用实例</h1>
      <AppInstanceControl />
      <DataTable />
    </Layout>
  );
}
