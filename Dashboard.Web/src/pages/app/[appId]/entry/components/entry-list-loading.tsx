import { VStack } from "@chakra-ui/react";
import EntryListItem from "./entry-list-item";
import { EntryDTO } from "../../../../../../api-client/Entry.client";

const LOADING_ENTRY: EntryDTO = {
  id: 0,
  name: "LOADING",
  description: "LOADING",
  appID: 0,
  deployedAt: 0,
};

export default function EntryListLoading() {
  return (
    <VStack alignItems="stretch">
      {[...new Array(3)].map((_, index) => (
        <EntryListItem entry={LOADING_ENTRY} key={index} isLoaded={false} />
      ))}
    </VStack>
  );
}
