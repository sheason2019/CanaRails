import { EntryMatcherDTO } from "../../../../../../../api-client/EntryMatcher.client";
import EntryMatcherListItem from "./entry-matcher-list-item";

const LOADING_ENTRY_MATCHER: EntryMatcherDTO = {
  id: 0,
  key: "LOADING",
  value: "LOADING",
  entryId: 0,
};

export default function EntryMatcherListLoading() {
  return (
    <>
      {[...new Array(3)].map((_, index) => (
        <EntryMatcherListItem
          key={index}
          matcher={LOADING_ENTRY_MATCHER}
          isLoaded={false}
        />
      ))}
    </>
  );
}
