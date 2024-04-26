import ErrorLabel from "../../../../components/form/error-label";

interface Props {
  name: string;
  label: string;
  type: "text" | "number";
  errors?: string[];
}

export default function FormInput(props: Props) {
  return (
    <label class="form-control w-full max-w-xs">
      <div class="label">
        <span class="label-text">{props.label}</span>
      </div>
      <input
        name={props.name}
        type={props.type}
        class="input input-bordered w-full max-w-xs text-sm"
      />
      <ErrorLabel errors={props.errors} />
    </label>
  );
}
