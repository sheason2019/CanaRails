import { JSX, createSignal } from "solid-js";
import { ZodFormattedError, z } from "zod";
import { appClient } from "../../../../clients";

const schema = z.object({
  name: z.string().min(1, "App 名称不能为空").max(24, "App 名称最多 24 位"),
  host: z.string().min(1, "访问地址不能为空"),
  port: z.number().int("映射端口必须为正整数").min(1, "映射端口必须为正整数"),
  description: z.string(),
});

export type FormError = ZodFormattedError<z.infer<typeof schema>>;

function handleParseFormData(formData: FormData) {
  const data = {
    name: formData.get("name")?.toString(),
    host: formData.get("host")?.toString(),
    port: Number(formData.get("port")?.toString()),
    description: formData.get("description")?.toString(),
  };

  return schema.safeParse(data);
}

export default function useCreateApp() {
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

    const resp = await appClient.create({ dto: { ...zodBundle.data, id: 0 } });
    console.log("handle submit", resp);
  };

  return {
    formError,
    handleSubmit,
  };
}
