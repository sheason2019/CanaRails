import { FormControl, FormLabel, Heading, Text } from "@chakra-ui/react";
import useEntryDetail from "../hook/use-entry-detail";

export default function EntryDetailInfo() {
  const { data } = useEntryDetail();

  return (
    <>
      <Heading size="md" className="mb-3">
        详情信息
      </Heading>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
        <FormControl>
          <FormLabel fontWeight="bold">ID</FormLabel>
          <Text>{data?.id}</Text>
        </FormControl>
        <FormControl>
          <FormLabel fontWeight="bold">流量入口名称</FormLabel>
          <Text>{data?.name}</Text>
        </FormControl>
      </div>
      <FormControl className="mt-2">
        <FormLabel fontWeight="bold">简介</FormLabel>
        <Text>{data?.description}</Text>
      </FormControl>
    </>
  );
}
