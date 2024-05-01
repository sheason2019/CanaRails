import { For, createMemo } from "solid-js";
import { z } from "zod";
import { CreateSimpleFormOptions } from "./typings";
import SimpleFormItem from "./simple-form-item";
import useSimpleForm from "./use-simple-form";

interface Props<T extends z.ZodRawShape> {
  schema: z.ZodObject<T>;
  options?: CreateSimpleFormOptions<T>;
}

export default function SimpleForm<T extends z.ZodRawShape>(props: Props<T>) {
  const { runAsync, validateFieldAsync, errors } = useSimpleForm(
    props.schema,
    props.options
  );

  const itemIndex = createMemo(() => {
    const declaredIndex = (props.options?.formIndex ?? []).map((item) =>
      item.toString()
    );
    const declaredSet = new Set(declaredIndex);
    return declaredIndex.concat(
      Object.keys(props.schema.shape).filter((item) => !declaredSet.has(item))
    );
  });

  return (
    <form
      onSubmit={async (e) => {
        e.preventDefault();
        const data = await runAsync(new FormData(e.currentTarget));
        if (data) {
          props.options?.onSubmit?.(data);
        }
      }}
    >
      <For each={itemIndex()}>
        {(name) => (
          <SimpleFormItem
            name={name}
            validateFieldAsync={validateFieldAsync}
            option={props.options?.formOptions?.[name]}
            errors={errors()[name]?._errors}
          />
        )}
      </For>
      <button class="btn btn-primary mt-8">
        {props.options?.submitText ?? "Submit"}
      </button>
    </form>
  );
}
