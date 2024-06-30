import { PropsWithChildren } from "react";

export default function PageContainer({ children }: PropsWithChildren) {
  return <div className="container mx-auto px-4">{children}</div>;
}
