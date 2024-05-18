import { FormControl, FormLabel, Heading, Text } from "@chakra-ui/react";
import useAppDetail from "../hooks/use-app-detail";

export default function AppDetailInfo() {
  const { data } = useAppDetail();

  return (
    <>
      <Heading size="lg" className="mt-6 mb-3">
        应用信息
      </Heading>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-2 mb-2">
        <FormControl>
          <FormLabel>ID</FormLabel>
          <Text>{data?.id}</Text>
        </FormControl>
        <FormControl>
          <FormLabel>App 名称</FormLabel>
          <Text>{data?.name}</Text>
        </FormControl>
      </div>
      <FormControl>
        <FormLabel>App 简介</FormLabel>
        <Text>{data?.description}</Text>
      </FormControl>
    </>
  );
}
