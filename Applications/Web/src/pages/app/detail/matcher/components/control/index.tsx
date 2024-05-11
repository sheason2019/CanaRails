import { z } from "zod";
import createSimpleForm from "../../../../../../components/form/create-simple-form";
import { appMatcherClient } from "../../../../../../clients";
import { useParams } from "@solidjs/router";
import useAppMatcherListQuery from "../list/hooks/use-app-matcher-list-query";

export default function AppMatcherControl() {
  const params = useParams();
  const query = useAppMatcherListQuery();
  let dialogEl: HTMLDialogElement | undefined;

  const { renderer } = createSimpleForm(
    z.object({
      host: z.string().min(1, "Host 不能为空"),
    }),
    {
      formOptions: {
        host: {
          label: "Host",
        },
      },
      submitText: "新建",
      async onSubmit(data) {
        await appMatcherClient.create(Number(params.id), {
          dto: { id: 0, host: data.host, appID: Number(params.id) },
        });
        dialogEl?.close();
        query.refetch();
      },
    }
  );

  return (
    <div>
      <button class="btn btn-primary" onClick={() => dialogEl?.showModal()}>
        新建匹配器
      </button>
      <dialog ref={dialogEl} class="modal">
        <div class="modal-box">
          <h3 class="font-bold text-lg">新建匹配器</h3>
          {renderer}
        </div>
        <form method="dialog" class="modal-backdrop">
          <button>close</button>
        </form>
      </dialog>
    </div>
  );
}
