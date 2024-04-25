import { ZodFormattedError, z } from "zod";

const schema = z.object({
  name: z.string().min(1, "App 名称不能为空").max(24, "App 名称最多 24 位"),
  host: z.string().min(1, "Host 不能为空"),
  registry: z
    .string()
    .url("无效的 Registry")
    .or(z.string().length(0))
    .or(z.undefined()),
  description: z.string(),
});

export type FormError = ZodFormattedError<z.infer<typeof schema>>;

export default function handleParseFormData(formData: FormData) {
  const data = {
    name: formData.get("name")?.toString(),
    host: formData.get("host")?.toString(),
    registry: formData.get("registry")?.toString(),
    description: formData.get("description")?.toString(),
  };

  return schema.safeParse(data);
}
