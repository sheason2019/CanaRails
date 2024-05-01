import type { z } from "zod";
import { DistinctArray } from "../../utils/shared-type-utils";

export interface FormItemOption {
  label?: string;
  type?: "text" | "textarea";
}

export type SchemaParseData<T extends z.ZodRawShape> = Awaited<
  ReturnType<z.ZodObject<T>["safeParseAsync"]>
>["data"];

export interface CreateSimpleFormOptions<T extends z.ZodRawShape> {
  formOptions?: { [key in keyof T]?: FormItemOption };
  formIndex?: DistinctArray<(keyof T)[]>;
  onSubmit?: (data: Exclude<SchemaParseData<T>, undefined>) => void;
  submitText?: string;
}
