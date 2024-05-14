import { For, Match, Switch } from "solid-js";
import { FormItemOption } from "./typings";
import useSimpleForm from "./use-simple-form";

interface Props {
  name: string;
  validateFieldAsync: ReturnType<typeof useSimpleForm>["validateFieldAsync"];
  errors?: string[];
  readOnly?: boolean;
  option?: FormItemOption;
}

export default function SimpleFormItem(props: Props) {
  return (
    <label
      classList={{
        "form-control w-full": true,
        "max-w-xs": props.option?.type !== "textarea",
      }}
    >
      <div class="label">
        <span class="label-text">{props.option?.label ?? props.name}</span>
      </div>
      <Switch
        fallback={
          <input
            name={props.name}
            type={props.option?.type}
            placeholder={props.option?.placeholder}
            readOnly={props.readOnly}
            onBlur={(e) =>
              props.validateFieldAsync(
                new FormData(e.currentTarget.form!),
                props.name
              )
            }
            class="input input-bordered w-full max-w-xs text-sm"
          />
        }
      >
        <Match when={props.option?.type === "textarea"}>
          <textarea
            name={props.name}
            placeholder={props.option?.placeholder}
            readOnly={props.readOnly}
            onBlur={(e) =>
              props.validateFieldAsync(
                new FormData(e.currentTarget.form!),
                props.name
              )
            }
            class="textarea textarea-bordered w-full"
          />
        </Match>
        <Match when={props.option?.type === "select"}>
          <select
            name={props.name}
            disabled={props.readOnly}
            class="select select-bordered w-full max-w-xs"
          >
            <option disabled selected hidden value="">
              {props.option?.placeholder}
            </option>
            {props.option?.type === "select" && (
              <For each={props.option.options}>
                {(option) => (
                  <option value={option.value}>
                    {option.label ?? option.value}
                  </option>
                )}
              </For>
            )}
          </select>
        </Match>
      </Switch>
      <div class="label">
        <span class="label-text-alt text-red-500">
          {props.errors?.join(",")}
        </span>
      </div>
    </label>
  );
}
