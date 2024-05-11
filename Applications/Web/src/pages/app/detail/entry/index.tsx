import { Suspense } from "solid-js";
import Layout from "../../../layout";
import AppEntryControl from "./components/control";
import EntryDefault from "./components/entry-default";
import EntryList from "./components/entry-list";
import SimpleLoadingSpinner from "../../../../components/simple-loading-spinner";

export default function AppEntryPage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">应用入口</h1>
      <AppEntryControl />
      <Suspense fallback={<SimpleLoadingSpinner />}>
        <EntryDefault />
        <EntryList />
      </Suspense>
    </Layout>
  );
}
