import { VStack } from "@chakra-ui/react";
import EntryListItem from "./entry-list-item";
import { EntryDTO } from "../../../../../../api-client";

const LOADING_ENTRY: EntryDTO = {
  id: 0,
  name: "LOADING",
  description: "LOADING",
  deployedAt: 0,
  matchers: [],
  appId: 0,
};

export default function EntryListLoading() {
  return (
    <VStack alignItems="stretch">
      {[...new Array(3)].map((_, index) => (
        <EntryListItem
          entry={LOADING_ENTRY}
          isDefault={false}
          key={index}
          isLoaded={false}
        />
      ))}
    </VStack>
  );
}
