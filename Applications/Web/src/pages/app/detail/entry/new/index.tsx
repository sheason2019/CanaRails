import { z } from "zod";
import { createMemo } from "solid-js";
import { useNavigate, useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import createSimpleForm from "../../../../../components/form/create-simple-form";
import Layout from "../../../../layout";
import { entryClient, imageClient } from "../../../../../clients";

export default function NewAppEntryPage() {
  const navigate = useNavigate();
  const params = useParams();
  const imageOptionQuery = createQuery(() => ({
    queryKey: ["image-option", params.id],
    queryFn: async () => {
      const images = await imageClient.list(Number(params.id));
      return images.map((image) => ({
        label: `${image.imageName}:${image.tagName}`,
        value: image.id,
      }));
    },
    suspense: true,
  }));

  const formRenderer = createMemo(() =>
    createSimpleForm(
      z.object({
        name: z.string().min(1, "入口名称不能为空"),
        imageID: z.string().regex(/^\d+$/).transform(Number),
        port: z.string().regex(/^\d+$/).transform(Number),
        description: z.string().default(""),
      }),
      {
        formOptions: {
          name: {
            label: "入口名称",
          },
          imageID: {
            label: "选择镜像",
            type: "select",
            placeholder: "请选择镜像",
            options: imageOptionQuery.data ?? [],
          },
          port: {
            label: "映射端口",
          },
          description: {
            label: "简介",
            type: "textarea",
          },
        },
        submitText: "创建入口",
        async onSubmit(data) {
          const response = await entryClient.create({
            dto: { id: 0, appID: Number(params.id), ...data },
          });
          navigate(`/app/${params.id}/entry/${response.id}`);
        },
      }
    )
  );

  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">新建应用入口</h1>
      {formRenderer()}
    </Layout>
  );
}
