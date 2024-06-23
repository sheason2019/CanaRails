import { Radio, RadioGroup, Stack } from "@chakra-ui/react";
import { ImageDTO } from "../../../../../../../api-client";
import useImageList from "../../../image/hooks/use-image-list";

interface Props {
  value: ImageDTO | null;
  onChange: (image: ImageDTO | null) => void;
}

export default function ImageSelector({ value, onChange }: Props) {
  const { data } = useImageList();

  return (
    <RadioGroup value={value?.id.toString()}>
      <Stack gap={3}>
        {data?.map((item) => (
          <Radio
            key={item.id}
            value={item.id.toString()}
            onChange={() => onChange(item)}
          >
            <p className="text-lg font-bold">{item.imageName}</p>
            <p>
              <span>ID {item.id}</span>
              <span className="mx-1">·</span>
              <span>{new Date(item.createdAt).toLocaleString()}</span>
            </p>
          </Radio>
        ))}
      </Stack>
    </RadioGroup>
  );
}
