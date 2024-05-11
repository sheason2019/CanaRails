import { z } from "zod";
import { createMemo } from "solid-js";
import { useNavigate, useParams } from "@solidjs/router";
import createSimpleForm from "../../../../../components/form/create-simple-form";
import Layout from "../../../../layout";
import { entryClient } from "../../../../../clients";

export default function NewAppEntryPage() {
  const navigate = useNavigate();
  const params = useParams();

  const formRenderer = createMemo(
    () =>
      createSimpleForm(
        z.object({
          name: z.string().min(1, "入口名称不能为空"),
          description: z.string().default(""),
        }),
        {
          formOptions: {
            name: {
              label: "入口名称",
            },
            description: {
              label: "简介",
              type: "textarea",
            },
          },
          submitText: "创建入口",
          async onSubmit(data) {
            const response = await entryClient.create({
              dto: {
                id: 0,
                appID: Number(params.id),
                deployedAt: 0,
                state: "",
                ...data,
              },
            });
            navigate(`/app/${params.id}/entry/${response.id}`);
          },
        }
      ).renderer
  );

  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">新建应用入口</h1>
      {formRenderer()}
    </Layout>
  );
}
