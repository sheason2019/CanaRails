import {
  Card,
  CardBody,
  Skeleton,
  Stat,
  StatLabel,
  StatNumber,
} from "@chakra-ui/react";
import { ReactNode } from "react";
import { Link } from "react-router-dom";

interface Props {
  label: ReactNode;
  value: ReactNode;
  to: string;
  isLoading: boolean;
}

export default function AppStatItem({ to, label, value, isLoading }: Props) {
  return (
    <Link to={to}>
      <Card _hover={{ shadow: "lg" }} className="transition-shadow">
        <CardBody>
          <Stat>
            <StatLabel>{label}</StatLabel>
            <StatNumber mt={2}>
              <Skeleton
                width={isLoading ? 16 : undefined}
                isLoaded={!isLoading}
              >
                {value}
              </Skeleton>
            </StatNumber>
          </Stat>
        </CardBody>
      </Card>
    </Link>
  );
}
