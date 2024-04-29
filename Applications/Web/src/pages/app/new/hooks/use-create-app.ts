import { createSignal } from "solid-js";
import { ZodFormattedError, z } from "zod";
import { appClient } from "../../../../clients";
import { createMutation } from "@tanstack/solid-query";
import { useNavigate } from "@solidjs/router";

const schema = z.object({
  name: z.string().min(1, "App 名称不能为空").max(24, "App 名称最多 24 位"),
  appID: z.string().min(1, "App ID 不能为空"),
  description: z.string(),
});

export type FormError = ZodFormattedError<z.infer<typeof schema>>;

function handleParseFormData(formData: FormData) {
  const data = {
    name: formData.get("name")?.toString(),
    appID: formData.get("app-id")?.toString(),
    description: formData.get("description")?.toString(),
  };

  return schema.safeParse(data);
}

export default function useCreateApp() {
  const navigate = useNavigate();
  const [formError, setFormError] = createSignal<FormError>();
  const handleSubmit = async (formData: FormData) => {
    const zodBundle = handleParseFormData(formData);
    if (!zodBundle.success) {
      setFormError(zodBundle.error.format());
      throw new Error("表单校验失败");
    }

    return await appClient.create({ dto: { ...zodBundle.data, id: 0 } });
  };

  const mutation = createMutation(() => ({
    mutationKey: ["create-app"],
    mutationFn: handleSubmit,
    onSuccess(data) {
      navigate(`/app/${data.id}`);
    },
  }));

  return {
    formError,
    mutation,
  };
}
