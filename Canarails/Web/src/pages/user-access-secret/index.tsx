import { Button, Heading } from "@chakra-ui/react";
import PageContainer from "../../components/page-container";

export default function UserAccessSecretPage() {
  return (
    <PageContainer>
      <Heading className="my-3">用户鉴权密钥</Heading>
      <Button colorScheme="blue">新建</Button>
    </PageContainer>
  );
}
