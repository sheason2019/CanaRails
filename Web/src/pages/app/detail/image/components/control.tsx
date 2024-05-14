import { useParams } from "@solidjs/router";

export default function AppImageControl() {
  const params = useParams();

  return (
    <div>
      <a class="btn btn-primary" href={`/app/${params.id}/image/new`}>
        创建镜像
      </a>
    </div>
  );
}
