import Layout from "../../layout";
import ErrorLabel from "../../../components/form/error-label";
import useCreateApp from "./hooks/use-create-app";

export default function NewAppPage() {
  const { mutation, formError } = useCreateApp();

  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">创建应用</h1>
      <form
        onSubmit={(e) => {
          e.preventDefault();
          return mutation.mutate(new FormData(e.currentTarget));
        }}
      >
        <label class="form-control w-full max-w-xs">
          <div class="label">
            <span class="label-text">应用名称</span>
          </div>
          <input
            name="name"
            type="text"
            readOnly={mutation.isPending}
            class="input input-bordered w-full max-w-xs text-sm"
          />
          <ErrorLabel errors={formError()?.name?._errors} />
        </label>
        <label class="form-control w-full max-w-xs">
          <div class="label">
            <span class="label-text">访问地址</span>
          </div>
          <div class="input input-bordered w-full max-w-xs text-sm flex items-center">
            <input
              type="text"
              readOnly={mutation.isPending}
              name="host"
              class="grow"
            />
            <p>.{location.host}</p>
          </div>
          <ErrorLabel errors={formError()?.host?._errors} />
        </label>
        <label class="form-control w-full">
          <div class="label">
            <span class="label-text">应用简介</span>
          </div>
          <textarea
            name="description"
            readOnly={mutation.isPending}
            placeholder="选填"
            class="textarea textarea-bordered w-full"
          />
          <ErrorLabel errors={formError()?.description?._errors} />
        </label>
        <button disabled={mutation.isPending} class="btn btn-primary mt-8">
          创建
        </button>
      </form>
    </Layout>
  );
}
