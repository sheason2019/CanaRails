import { Card, CardBody, Skeleton } from "@chakra-ui/react";
import { ContainerDTO } from "../../../../../../../api-client/Container.client";

interface Props {
  container: ContainerDTO;
  isLoaded: boolean;
}

export default function EntryContainerlistItem({ container, isLoaded }: Props) {
  return (
    <Card>
      <Skeleton isLoaded={isLoaded}>
        <CardBody>
          <p>{container.containerId}</p>
          <p>Version {container.version}</p>
        </CardBody>
      </Skeleton>
    </Card>
  );
}
