import type { z } from "zod";

interface FormItemTextOption {
  type?: "text" | "textarea";
}

interface FormItemSelectOption {
  type: "select";
  options: { label: string; value: any }[];
}

export type FormItemOption = {
  label?: string;
  placeholder?: string;
} & (FormItemTextOption | FormItemSelectOption);

export type SchemaParseData<T extends z.ZodRawShape> = Awaited<
  ReturnType<z.ZodObject<T>["safeParseAsync"]>
>["data"];

export interface CreateSimpleFormOptions<T extends z.ZodRawShape> {
  formOptions?: { [key in keyof T]?: FormItemOption };
  formIndex?: (keyof T)[];
  onSubmit?: (data: Exclude<SchemaParseData<T>, undefined>) => void;
  submitText?: string;
}
