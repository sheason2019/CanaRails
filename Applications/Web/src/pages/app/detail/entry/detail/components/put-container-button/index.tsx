import { createMemo } from "solid-js";
import { z } from "zod";
import { useParams } from "@solidjs/router";
import { createQuery } from "@tanstack/solid-query";
import createSimpleForm from "../../../../../../../components/form/create-simple-form";
import { entryClient, imageClient } from "../../../../../../../clients";

export default function PutContainerButton() {
  const params = useParams();
  let dialogEl: HTMLDialogElement | undefined;

  const query = createQuery(() => ({
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
        imageID: z.string().regex(/^\d+$/).transform(Number),
        port: z.string().regex(/^\d+$/).transform(Number),
      }),
      {
        formOptions: {
          imageID: {
            label: "镜像",
            type: "select",
            options: query.data ?? [],
          },
          port: {
            label: "映射端口",
          },
        },
        submitText: "变更",
        async onSubmit(data) {
          await entryClient.putContainer(Number(params.entryID), {
            dto: {
              id: 0,
              imageID: data.imageID,
              entryID: Number(params.entryID),
              port: data.port,
              containerID: "",
            },
          });
          dialogEl?.close();
        },
      }
    )
  );

  return (
    <>
      <button class="btn" onClick={() => dialogEl?.showModal()}>
        变更容器
      </button>
      <dialog ref={dialogEl} class="modal">
        <div class="modal-box">
          <h3 class="font-bold text-lg">变更容器</h3>
          <p class="py-4">切换该入口使用的容器及其映射端口</p>
          {formRenderer()}
        </div>
        <form method="dialog" class="modal-backdrop">
          <button>close</button>
        </form>
      </dialog>
    </>
  );
}
