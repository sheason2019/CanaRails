import { Card, CardBody, Stat, StatLabel, StatNumber } from "@chakra-ui/react";
import { ReactNode } from "react";
import { Link } from "react-router-dom";

interface Props {
  label: ReactNode;
  value: ReactNode;
  to: string;
}

export default function AppStatItem({ to, label, value }: Props) {
  return (
    <Link to={to}>
      <Card _hover={{ shadow: "lg" }} className="transition-shadow">
        <CardBody>
          <Stat>
            <StatLabel>{label}</StatLabel>
            <StatNumber>{value}</StatNumber>
          </Stat>
        </CardBody>
      </Card>
    </Link>
  );
}
