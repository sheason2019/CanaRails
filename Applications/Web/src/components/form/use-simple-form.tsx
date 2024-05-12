import { createSignal } from "solid-js";
import { z } from "zod";
import { CreateSimpleFormOptions } from "./typings";

export default function useSimpleForm<T extends z.ZodRawShape>(
  schema: z.ZodObject<T>,
  options?: CreateSimpleFormOptions<T>
) {
  options;
  const [errors, setErrors] = createSignal<Partial<z.ZodFormattedError<T>>>({});

  /**
   * 从 formData 中校验表单，若成功则返回表单数据
   * 否则返回 null
   */
  const runAsync = async (formData: FormData) => {
    const data = Object.fromEntries(formData.entries());
    const res = await schema.safeParseAsync(data);
    if (!res.success) {
      setErrors(() => res.error.format());
      return null;
    }

    return res.data;
  };

  /**
   * 校验单一字段
   */
  const validateFieldAsync = async (formData: FormData, fieldName: string) => {
    const data = Object.fromEntries(formData.entries());
    const res = await schema
      .pick({ [fieldName]: true } as any)
      .safeParseAsync(data);

    return setErrors((prev) => ({
      ...prev,
      [fieldName]: res.success
        ? undefined
        : (res.error.format() as any)?.[fieldName],
    }));
  };

  return {
    runAsync,
    validateFieldAsync,
    errors,
  };
}
