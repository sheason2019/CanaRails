import { z } from "zod";
import createSimpleForm from "../../../../../components/form/create-simple-form";
import Layout from "../../../../layout";
import { imageClient } from "../../../../../clients";
import { useNavigate, useParams } from "@solidjs/router";

export default function NewAppImagePage() {
  const params = useParams();
  const navigate = useNavigate();

  const formRenderer = createSimpleForm(
    z.object({
      registry: z.string().default(""),
      imageName: z.string().min(1, "镜像名称不能为空").default(""),
      tagName: z.string().min(1, "Tag名称不能为空").default(""),
    }),
    {
      formOptions: {
        registry: {
          label: "镜像仓库",
          placeholder: "选填",
        },
        imageName: {
          label: "镜像名称",
        },
        tagName: {
          label: "镜像Tag",
        },
      },
      submitText: "创建镜像",
      async onSubmit(data) {
        await imageClient.create({
          dto: {
            id: 0,
            appID: Number(params.id),
            ...data,
          },
        });
        navigate(`/app/${params.id}/image`);
      },
    }
  );

  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">创建应用镜像</h1>
      {formRenderer}
    </Layout>
  );
}
