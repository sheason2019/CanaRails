import { Button, Flex, Heading } from "@chakra-ui/react";

export default function EntryContainerList() {
  return (
    <>
      <Flex className="my-3" alignItems="center">
        <Heading size="md" className="grow">
          容器一览
        </Heading>
        <Button>配置变更</Button>
      </Flex>
    </>
  );
}
