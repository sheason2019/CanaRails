import { JSX } from "solid-js";

export default function InfoItem(props: { label: string; value: JSX.Element }) {
  return (
    <div>
      <div class="font-bold mb-1">{props.label}</div>
      <div class="text-gray-500 text-sm">{props.value}</div>
    </div>
  );
}
