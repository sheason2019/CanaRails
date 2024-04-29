import { useParams } from "@solidjs/router";

export default function AppEntryControl() {
  const params = useParams();

  return (
    <div>
      <a href={`/app/${params.id}/entry/new`} class="btn btn-primary">
        新建应用入口
      </a>
    </div>
  );
}
