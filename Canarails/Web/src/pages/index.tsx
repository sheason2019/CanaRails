import { Button } from "@chakra-ui/react";
import cx from "classnames";
import { Link } from "react-router-dom";

export default function HomePage() {
  return (
    <div
      className={cx(
        "w-full h-screen bg-slate-200",
        "flex flex-col justify-center items-center"
      )}
    >
      <h1 className="text-5xl font-bold">CanaRails</h1>
      <p className="py-6">Canary DEV environments on the Rails.</p>
      <Link to="/dashboard">
        <Button colorScheme="blue" className="px-8 py-6 text-lg">
          Get Started
        </Button>
      </Link>
    </div>
  );
}
