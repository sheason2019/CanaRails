import Layout from "../../../layout";
import AppEntryControl from "./components/control";
import EntryList from "./components/entry-list";

export default function AppEntryPage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">应用入口</h1>
      <AppEntryControl />
      <EntryList />
    </Layout>
  );
}
