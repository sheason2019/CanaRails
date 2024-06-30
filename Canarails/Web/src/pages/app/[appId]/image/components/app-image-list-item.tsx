import { Card, CardBody, Flex, Skeleton } from "@chakra-ui/react";
import { ImageDTO } from "../../../../../../api-client";

interface Props {
  image: ImageDTO;
  isLoaded: boolean;
}

export default function AppImageListItem({ image, isLoaded }: Props) {
  return (
    <Card>
      <Skeleton isLoaded={isLoaded}>
        <CardBody>
          <Flex className="items-baseline mb-1">
            <p className="text-lg font-bold mr-2">{image.imageName}</p>
            <p className="text-sm font-bold text-gray-500">ID {image.id}</p>
          </Flex>
          <Flex>
            <p className="text-gray-500">{new Date(image.createdAt).toLocaleString()}</p>
          </Flex>
        </CardBody>
      </Skeleton>
    </Card>
  );
}
