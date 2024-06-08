import { Card, CardBody, Flex, Skeleton, Stack } from "@chakra-ui/react";
import { EntryMatcherDTO } from "../../../../../../../api-client/Entry.client";

interface Props {
  matcher: EntryMatcherDTO;
  isLoaded: boolean;
}

export default function EntryMatcherListItem({ matcher, isLoaded }: Props) {
  return (
    <Card>
      <Skeleton isLoaded={isLoaded}>
        <CardBody as={Stack} gap={1}>
          <Flex>
            <p className="w-20 font-bold">KEY</p>
            <p>{matcher.key}</p>
          </Flex>
          <Flex>
            <p className="w-20 font-bold">VALUE</p>
            <p>{matcher.value}</p>
          </Flex>
        </CardBody>
      </Skeleton>
    </Card>
  );
}
