import { SearchIcon } from "@chakra-ui/icons";
import {
  Button,
  Input,
  InputGroup,
  InputRightElement,
  Stack,
} from "@chakra-ui/react";
import { Link } from "react-router-dom";

export default function AppControl() {
  return (
    <Stack direction="row">
      <Link to="/app/new">
        <Button width={28} colorScheme="blue">
          新建
        </Button>
      </Link>
      <InputGroup>
        <Input variant="outline" placeholder="搜索" />
        <InputRightElement>
          <SearchIcon color="gray.500" />
        </InputRightElement>
      </InputGroup>
    </Stack>
  );
}
