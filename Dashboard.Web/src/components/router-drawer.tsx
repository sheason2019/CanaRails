import { HamburgerIcon } from "@chakra-ui/icons";
import {
  Drawer,
  DrawerBody,
  DrawerHeader,
  DrawerOverlay,
  DrawerContent,
  DrawerCloseButton,
  useDisclosure,
  IconButton,
  List,
  ListItem,
  Button,
} from "@chakra-ui/react";
import { useRef } from "react";
import { Link } from "react-router-dom";

interface IDrawerRotuer {
  label: string;
  to: string;
}

const drawerRouters: IDrawerRotuer[] = [
  {
    label: "Dashboard",
    to: "/dashboard",
  },
  {
    label: "App",
    to: "/app",
  },
];

export default function RouterDrawer() {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const btnRef = useRef<HTMLButtonElement>(null);

  return (
    <>
      <IconButton
        ref={btnRef}
        onClick={onOpen}
        variant="ghost"
        aria-label="open router menu"
        height={12}
        width={12}
      >
        <HamburgerIcon />
      </IconButton>
      <Drawer
        isOpen={isOpen}
        placement="left"
        onClose={onClose}
        finalFocusRef={btnRef}
      >
        <DrawerOverlay />
        <DrawerContent>
          <DrawerCloseButton width={12} height={12} />
          <DrawerHeader>菜单</DrawerHeader>

          <DrawerBody>
            <List>
              {drawerRouters.map((route) => (
                <ListItem key={route.to}>
                  <Link to={route.to} onClick={onClose}>
                    <Button
                      variant="ghost"
                      justifyContent="flex-start"
                      className="w-full"
                    >
                      {route.label}
                    </Button>
                  </Link>
                </ListItem>
              ))}
            </List>
          </DrawerBody>
        </DrawerContent>
      </Drawer>
    </>
  );
}
