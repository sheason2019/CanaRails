import { ParentProps } from "solid-js";
import NavBar from "./components/nav-bar";

export default function Layout(props: ParentProps) {
  return (
    <>
      <NavBar />
      <main class="container px-4 mt-4 mx-auto">{props.children}</main>
    </>
  );
}
