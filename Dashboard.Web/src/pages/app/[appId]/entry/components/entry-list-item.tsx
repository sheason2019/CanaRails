import { Card, CardBody, Flex, Skeleton, Text } from "@chakra-ui/react";
import { EntryDTO } from "../../../../../../api-client/Entry.client";
import { Link } from "react-router-dom";

interface Props {
  entry: EntryDTO;
  isLoaded: boolean;
}

export default function EntryListItem({ entry, isLoaded }: Props) {
  return (
    <Card
      as={Link}
      to={`/app/${entry.appID}/entry/${entry.id}`}
      _hover={{ shadow: "lg" }}
      className="transition-shadow"
    >
      <Skeleton isLoaded={isLoaded}>
        <CardBody>
          <Flex gap={2} alignItems="baseline" className="mb-2">
            <p className="text-lg font-bold">{entry.name}</p>
            <p className="text-gray-500 font-bold text-sm">ID {entry.id}</p>
          </Flex>
          <Text>{entry.description}</Text>
        </CardBody>
      </Skeleton>
    </Card>
  );
}
