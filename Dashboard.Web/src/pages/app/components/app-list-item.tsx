import { Card, Skeleton, CardBody, Flex, Text } from "@chakra-ui/react";
import { AppDTO } from "../../../../api-client/App.client";
import { Link } from "react-router-dom";

interface Props {
  app: AppDTO;
  isLoaded: boolean;
}

export default function AppListItem({ app, isLoaded }: Props) {
  return (
    <Link to={isLoaded ? `/app/${app.id}` : "#"}>
      <Card>
        <Skeleton isLoaded={isLoaded}>
          <CardBody>
            <Flex className="items-baseline gap-2">
              <p className="text-lg font-bold">{app.name}</p>
              <p className="text-gray-500 font-bold text-sm">ID {app.id}</p>
            </Flex>
            <Text className="mt-1">{app.description}</Text>
          </CardBody>
        </Skeleton>
      </Card>
    </Link>
  );
}
