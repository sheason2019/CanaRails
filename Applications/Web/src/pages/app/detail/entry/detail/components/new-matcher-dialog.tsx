import { z } from "zod";
import createSimpleForm from "../../../../../../components/form/create-simple-form";

interface Props {
  ref?: HTMLDialogElement;
}

export default function NewMatcherDialog(props: Props) {
  const { renderer } = createSimpleForm(
    z.object({
      key: z.string().min(1, "key 不能为空"),
      value: z.string().min(1, "value 不能为空"),
    }),
    {
      formIndex: ["key", "value"],
      submitText: "提交",
      onSubmit(data) {
        console.log(data);
        props.ref?.close();
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
        <button>close</button>
      </form>
    </dialog>
  );
}
