import { Link, Outlet } from "react-router-dom";
import { Button } from "@chakra-ui/react";
import RouterDrawer from "./router-drawer";

export default function AdminLayout() {
  return (
    <>
      <div className="border-b flex items-center h-16 px-4">
        <RouterDrawer />
        <Link to="/">
          <Button variant="ghost" colorScheme="gray" height={12}>
            <p className="text-xl">CanaRails</p>
          </Button>
        </Link>
      </div>
      <Outlet />
    </>
  );
}
