import { Show } from "solid-js";

interface Props {
  errors: string[] | undefined;
}

export default function ErrorLabel(props: Props) {
  return (
    <Show when={props.errors}>
      <div class="label">
        <span class="label-text-alt text-red-500">
          {props.errors?.join(",")}
        </span>
      </div>
    </Show>
  );
}
