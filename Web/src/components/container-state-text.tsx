interface Props {
  state?: string;
}

export default function ContainerStateText(props: Props) {
  return (
    <div
      classList={{
        "text-gray-400": props.state === "exited",
        "text-green-500": props.state === "running",
      }}
    >
      {ContainerStateMapper(props.state)}
    </div>
  );
}

function ContainerStateMapper(state: string | undefined = ""): string {
  switch (state) {
    case "exited":
      return "已退出";
    case "running":
      return "运行中";
    default:
      return state;
  }
}
