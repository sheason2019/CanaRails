import { Flex, Heading, IconButton } from "@chakra-ui/react";
import PageContainer from "../../../components/page-container";
import { ArrowBackIcon } from "@chakra-ui/icons";
import NewAppForm from "./components/new-app-form";

export default function NewAppPage() {
  return (
    <PageContainer>
      <Flex className="items-center" gap={2}>
        <IconButton
          aria-label="route-back"
          variant="ghost"
          size="lg"
          onClick={() => history.back()}
        >
          <ArrowBackIcon />
        </IconButton>
        <Heading className="my-3">新建应用</Heading>
      </Flex>
      <NewAppForm />
    </PageContainer>
  );
}
