import { EntryDTO } from "../../../../../../api-client/Entry.client";
import ContainerStateText from "../../../../../components/container-state-text";
import EntryCardControlButton from "./entry-card-control-button";

interface Props {
  entry: EntryDTO;
}

export default function EntryCard(props: Props) {
  return (
    <a class="card shadow-md">
      <div class="card-body pb-4">
        <div class="flex items-center">
          <div class="grow text-lg font-bold">{props.entry.name}</div>
          <EntryCardControlButton entry={props.entry} />
        </div>
        <div>{props.entry.description}</div>
      </div>
      <div class="card-body py-3 bg-base-200">
        <div class="grid grid-cols-3 items-center">
          <div class="text-left text-sm">
            <span class="text-gray-400">ID {props.entry.id}</span>
          </div>
          <div class="text-center text-sm whitespace-nowrap text-ellipsis overflow-hidden">
            {new Date(props.entry.deployedAt).toLocaleString()} 更新
          </div>
          <div class="text-right">
            <ContainerStateText state={props.entry.state} />
          </div>
        </div>
      </div>
    </a>
  );
}
