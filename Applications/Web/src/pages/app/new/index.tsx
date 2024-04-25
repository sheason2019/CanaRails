import { JSX, createSignal } from "solid-js";
import Layout from "../../layout";
import handleParseFormData, { FormError } from "./methods/handle-validate";
import ErrorLabel from "../../../components/form/error-label";

export default function NewAppPage() {
  const [formError, setFormError] = createSignal<FormError>();
  const handleSubmit: JSX.EventHandlerUnion<HTMLFormElement, Event> = async (
    e
  ) => {
    e.preventDefault();

    const formData = new FormData(e.currentTarget);
    const zodBundle = handleParseFormData(formData);
    if (!zodBundle.success) {
      return setFormError(zodBundle.error.format());
    }

    console.log("handle submit", zodBundle.data);
  };

  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">创建应用</h1>
      <form onSubmit={handleSubmit}>
        <label class="form-control w-full max-w-xs">
          <div class="label">
            <span class="label-text">应用名称</span>
          </div>
          <input
            name="name"
            type="text"
            class="input input-bordered w-full max-w-xs text-sm"
          />
          <ErrorLabel errors={formError()?.name?._errors} />
        </label>
        <label class="form-control w-full max-w-xs">
          <div class="label">
            <span class="label-text">Host</span>
          </div>

          <div class="input input-bordered w-full max-w-xs text-sm flex items-center">
            <input type="text" name="host" class="grow" />
            <p>.{location.host}</p>
          </div>
          <ErrorLabel errors={formError()?.host?._errors} />
        </label>
        <label class="form-control w-full max-w-xs">
          <div class="label">
            <span class="label-text">镜像源</span>
          </div>
          <input
            type="text"
            name="registry"
            placeholder="默认使用官方源"
            class="input input-bordered w-full max-w-xs text-sm"
          />
          <ErrorLabel errors={formError()?.registry?._errors} />
        </label>
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
