import { z } from "zod";
import { CreateSimpleFormOptions } from "./typings";
import SimpleForm from "./simple-form";

// 通过函数的类型提示绑定 shcema 和 options
export default function createSimpleForm<T extends z.ZodRawShape>(
  schema: z.ZodObject<T>,
  options?: CreateSimpleFormOptions<T>
) {
  return <SimpleForm schema={schema} options={options} />;
}
