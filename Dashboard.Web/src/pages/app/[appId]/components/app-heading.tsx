import { Skeleton, Heading } from "@chakra-ui/react";
import useAppDetail from "../hooks/use-app-detail";

export default function AppHeading() {
  const { data, isLoading } = useAppDetail();

  return (
    <Skeleton isLoaded={!isLoading}>
      <Heading className="my-3">{data?.name}</Heading>
    </Skeleton>
  );
}
