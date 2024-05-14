import { createMutation } from "@tanstack/solid-query";
import { EntryMatcherDTO } from "../../../../../../../api-client/Entry.client";
import { entryClient } from "../../../../../../clients";
import { useParams } from "@solidjs/router";
import useEntryMatcherList from "../../hooks/use-entry-matcher-list";

interface Props {
  entryMatcher: EntryMatcherDTO;
}

export default function EntryMatcherControl(props: Props) {
  const params = useParams();
  const query = useEntryMatcherList();
  let details: HTMLDetailsElement | undefined;

  const handleClose = () => {
    details!.open = false;
  };

  const mutateDelete = createMutation(() => ({
    mutationFn: () =>
      entryClient.deleteMatcher(Number(params.entryID), props.entryMatcher.id),
    onSuccess() {
      query.refetch();
    },
  }));

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
        <li onClick={() => mutateDelete.mutate()}>
          <a class="text-red-500">删除</a>
        </li>
      </ul>
    </details>
  );
}
