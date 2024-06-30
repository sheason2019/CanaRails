import {
  Input,
  InputGroup,
  InputRightElement,
  Stack,
} from "@chakra-ui/react";
import { SearchIcon } from "@chakra-ui/icons";
import AppImageCreateButton from "./app-image-create-button";

export default function AppImageControl() {
  return (
    <Stack direction="row" className="mb-6">
      <AppImageCreateButton />
      <InputGroup>
        <Input variant="outline" placeholder="搜索" />
        <InputRightElement>
          <SearchIcon color="gray.500" />
        </InputRightElement>
      </InputGroup>
    </Stack>
  );
}
