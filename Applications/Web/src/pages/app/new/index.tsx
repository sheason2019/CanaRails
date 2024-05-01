import { z } from "zod";

import Layout from "../../layout";
import createSimpleForm from "../../../components/form/create-simple-form";
import { appClient } from "../../../clients";
import { useNavigate } from "@solidjs/router";

export default function NewAppPage() {
  const navigate = useNavigate();
  const formRenderer = createSimpleForm(
    z.object({
      name: z.string().min(1, "App 名称不能为空").max(24, "App 名称最多 24 位"),
      description: z.string(),
    }),
    {
      formOptions: {
        name: {
          label: "应用名称",
        },
        description: {
          label: "应用简介",
          type: "textarea",
        },
      },
      formIndex: ["name", "description"],
      submitText: "创建应用",
      async onSubmit(data) {
        const response = await appClient.create({ dto: { ...data, id: 0 } });
        navigate(`/app/${response.id}`);
      },
    }
  );

  return (
    <Layout>
      <h1 class="text-3xl font-bold mb-4">创建应用</h1>
      {formRenderer}
    </Layout>
  );
}
