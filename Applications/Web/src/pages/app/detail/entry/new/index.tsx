import Layout from "../../../../layout";

export default function NewAppEntryPage() {
  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">新建应用入口</h1>
      <form
        onSubmit={(e) => {
          e.preventDefault();
        }}
      >
        {/** TODO: 添加 Entry 表单 */}
      </form>
    </Layout>
  );
}
