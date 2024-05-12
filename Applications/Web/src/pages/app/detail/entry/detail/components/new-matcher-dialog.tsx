import { z } from "zod";
import createSimpleForm from "../../../../../../components/form/create-simple-form";
import { entryClient } from "../../../../../../clients";
import { useParams } from "@solidjs/router";
import useEntryMatcherList from "../../hooks/use-entry-matcher-list";

interface Props {
  ref?: HTMLDialogElement;
}

export default function NewMatcherDialog(props: Props) {
  const params = useParams();
  let closeBtn: HTMLButtonElement | undefined;

  const query = useEntryMatcherList();

  const { renderer } = createSimpleForm(
    z.object({
      key: z.string().min(1, "key 不能为空"),
      value: z.string().min(1, "value 不能为空"),
    }),
    {
      formIndex: ["key", "value"],
      submitText: "提交",
      async onSubmit(data) {
        await entryClient.putMatcher(Number(params.entryID), {
          id: 0,
          key: data.key,
          value: data.value,
          entryID: Number(params.entryID),
        });
        await query.refetch();
        closeBtn?.click();
      },
    }
  );

  return (
    <dialog class="modal" ref={props.ref}>
      <div class="modal-box">
        <h3 class="font-bold text-lg">添加入口匹配器</h3>
        <div class="py-4">{renderer}</div>
      </div>
      <form method="dialog" class="modal-backdrop">
        <button ref={closeBtn}>close</button>
      </form>
    </dialog>
  );
}
