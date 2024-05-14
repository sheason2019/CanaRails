import { useParams } from "@solidjs/router";
import { createMutation } from "@tanstack/solid-query";
import { EntryDTO } from "../../../../../../api-client/Entry.client";
import { entryClient } from "../../../../../clients";
import useEntryList from "../hooks/use-entry-list";
import useEntryDefault from "../hooks/use-entry-default";

interface Props {
  entry: EntryDTO;
}

export default function EntryCardControlButton(props: Props) {
  const params = useParams();

  let details: HTMLDetailsElement | undefined;

  const queryList = useEntryList();
  const queryDefault = useEntryDefault();

  const mutateDefault = createMutation(() => ({
    mutationFn: async () => {
      await entryClient.putDefaultEntry(Number(params.id), props.entry.id);
    },
    onSuccess() {
      queryList.refetch();
      queryDefault.refetch();
    },
  }));

  const handleClose = () => {
    details!.open = false;
  };

  return (
    <details class="dropdown dropdown-bottom dropdown-end" ref={details}>
      <summary class="btn btn-ghost btn-square">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          class="inline-block w-5 h-5 stroke-current"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M5 12h.01M12 12h.01M19 12h.01M6 12a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0z"
          ></path>
        </svg>
      </summary>
      <div
        class="fixed inset-0 z-50 bg-black bg-opacity-20"
        onClick={handleClose}
      />
      <ul class="p-2 shadow menu dropdown-content z-50 bg-base-100 rounded-box w-52">
        <li>
          <a href={`/app/${params.id}/entry/${props.entry.id}`}>查看详情</a>
        </li>
        <li
          onClick={async () => {
            await mutateDefault.mutateAsync();
            handleClose();
          }}
        >
          <a>设置为默认入口</a>
        </li>
        <li>
          <a>停用入口</a>
        </li>
        <li>
          <a class="text-red-500">删除入口</a>
        </li>
      </ul>
    </details>
  );
}
