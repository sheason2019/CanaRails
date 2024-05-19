import { Flex, Heading } from "@chakra-ui/react";
import EntryContainerUpdateButton from "./entry-container-update-button";

export default function EntryContainerList() {
  return (
    <>
      <Flex className="my-3" alignItems="center">
        <Heading size="md" className="grow">
          容器一览
        </Heading>
        <EntryContainerUpdateButton />
      </Flex>
    </>
  );
}
