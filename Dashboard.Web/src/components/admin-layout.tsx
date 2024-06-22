import { Link, Outlet } from "react-router-dom";
import { Button } from "@chakra-ui/react";
import RouterDrawer from "./router-drawer";
import UserButton from "./user-button";

export default function AdminLayout() {
  return (
    <>
      <div className="border-b flex items-center h-16 px-4 shrink-0">
        <RouterDrawer />
        <Link to="/">
          <Button variant="ghost" colorScheme="gray" height={12}>
            <p className="text-xl">CanaRails</p>
          </Button>
        </Link>
        <div className="flex-1" />
        <UserButton />
      </div>
      <Outlet />
    </>
  );
}
