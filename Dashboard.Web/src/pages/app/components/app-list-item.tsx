import { Card, Skeleton, CardBody } from "@chakra-ui/react";
import { AppDTO } from "../../../../api-client/App.client";

interface Props {
  app: AppDTO;
}

export default function AppListItem({ app }: Props) {
  return (
    <Card>
      <Skeleton>
        <CardBody>{app.id}</CardBody>
      </Skeleton>
    </Card>
  );
}
