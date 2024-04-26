import Layout from "../../layout";
import ErrorLabel from "../../../components/form/error-label";
import FormInput from "./components/form-input";
import useCreateApp from "./hooks/use-create-app";

export default function NewAppPage() {
  const { handleSubmit, formError } = useCreateApp();

  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">创建应用</h1>
      <form onSubmit={handleSubmit}>
        <FormInput
          name="name"
          label="应用名称"
          type="text"
          errors={formError()?.name?._errors}
        />
        <label class="form-control w-full max-w-xs">
          <div class="label">
            <span class="label-text">访问地址</span>
          </div>
          <div class="input input-bordered w-full max-w-xs text-sm flex items-center">
            <input type="text" name="host" class="grow" />
            <p>.{location.host}</p>
          </div>
          <ErrorLabel errors={formError()?.host?._errors} />
        </label>
        <FormInput
          name="port"
          label="映射端口"
          type="number"
          errors={formError()?.port?._errors}
        />
        <label class="form-control w-full">
          <div class="label">
            <span class="label-text">应用简介</span>
          </div>
          <textarea
            name="description"
            placeholder="选填"
            class="textarea textarea-bordered w-full"
          />
          <ErrorLabel errors={formError()?.description?._errors} />
        </label>
        <button class="btn btn-primary mt-8">创建</button>
      </form>
    </Layout>
  );
}
