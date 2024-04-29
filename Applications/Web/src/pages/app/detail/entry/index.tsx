import Layout from "../../../layout";
import AppEntryControl from "./components/control";
import DataTable from "./components/data-table";

export default function AppEntryPage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">应用入口</h1>
      <AppEntryControl />
      <DataTable />
    </Layout>
  );
}
