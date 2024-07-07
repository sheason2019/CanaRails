import {
  Box,
  Button,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Input,
  Modal,
  ModalBody,
  ModalContent,
  ModalFooter,
  ModalHeader,
  ModalOverlay,
  Stack,
  Step,
  StepIcon,
  StepIndicator,
  StepNumber,
  StepSeparator,
  StepStatus,
  StepTitle,
  Stepper,
  useDisclosure,
  useSteps,
  useToast,
} from "@chakra-ui/react";
import ImageSelector from "./image-selector";
import { useFormik } from "formik";
import { useState } from "react";
import * as y from "yup";
import { ApiException, ImageDTO } from "../../../../../../../api-client";
import { entryVersionClient } from "../../../../../../api";
import { useParams } from "react-router-dom";
import usePublishOrderList from "../hook/use-publish-order-list";

export default function PublishOrderCreateButton() {
  const toast = useToast({
    position: "bottom-right",
    variant: "left-accent",
  });
  const { entryId } = useParams();
  const { mutate } = usePublishOrderList();
  const [image, setImage] = useState<ImageDTO | null>(null);
  const { isOpen, onClose, onOpen } = useDisclosure();

  const formik = useFormik({
    initialValues: {
      port: 0,
      replica: 0,
    },
    validationSchema: y.object({
      port: y
        .number()
        .integer("映射端口号必须为正整数")
        .min(1, "映射端口号必须为正整数"),
      replica: y
        .number()
        .integer("实例数量必须为正整数")
        .min(1, "实例数量必须为正整数"),
    }),
    async onSubmit(values) {
      try {
        await entryVersionClient.create({
          id: 0,
          imageId: image!.id,
          entryId: Number(entryId),
          port: values.port,
          replica: values.replica,
        });
        onClose();
        mutate();
        toast({
          status: "success",
          title: "创建成功",
        });
      } catch (e) {
        const isApiException = e instanceof ApiException;
        toast({
          status: "error",
          title: isApiException ? "请求失败" : "未知错误",
          description: isApiException ? e.response : String(e),
        });
      }
    },
  });

  const steps = [
    {
      label: "选择镜像",
      content: <ImageSelector value={image} onChange={(v) => setImage(v)} />,
    },
    {
      label: "填写配置",
      content: (
        <Stack gap={4}>
          <FormControl>
            <FormLabel>选择镜像</FormLabel>
            <p>{image?.imageName}</p>
          </FormControl>
          <FormControl isInvalid={!!formik.errors.port}>
            <FormLabel htmlFor="put-container__port">映射端口</FormLabel>
            <Input
              name="port"
              type="number"
              id="put-container__port"
              value={formik.values.port}
              onChange={formik.handleChange}
            />
            <FormErrorMessage>{formik.errors.port}</FormErrorMessage>
          </FormControl>
          <FormControl isInvalid={!!formik.errors.replica}>
            <FormLabel htmlFor="put-container__replica">实例数量</FormLabel>
            <Input
              name="replica"
              type="number"
              id="put-container__replica"
              value={formik.values.replica}
              onChange={formik.handleChange}
            />
            <FormErrorMessage>{formik.errors.replica}</FormErrorMessage>
          </FormControl>
        </Stack>
      ),
    },
  ];

  const { activeStep, goToNext, goToPrevious } = useSteps({
    index: 0,
    count: steps.length,
  });

  return (
    <>
      <Button onClick={onOpen}>创建</Button>
      <Modal isOpen={isOpen} onClose={onClose} size="xl" isCentered>
        <ModalOverlay />
        <ModalContent mx={4}>
          <form onSubmit={formik.handleSubmit}>
            <ModalHeader>
              <Stepper index={activeStep}>
                {steps.map((step) => (
                  <Step key={step.label}>
                    <StepIndicator>
                      <StepStatus
                        complete={<StepIcon />}
                        incomplete={<StepNumber />}
                        active={<StepNumber />}
                      />
                    </StepIndicator>
                    <Box flexShrink="0">
                      <StepTitle>
                        <p className="text-sm sm:text-base">{step.label}</p>
                      </StepTitle>
                    </Box>
                    <StepSeparator />
                  </Step>
                ))}
              </Stepper>
            </ModalHeader>

            <ModalBody>{steps[activeStep].content}</ModalBody>
            <ModalFooter gap={2}>
              {activeStep === 0 ? (
                <Button type="button" onClick={onClose}>
                  取消
                </Button>
              ) : (
                <Button type="button" onClick={goToPrevious}>
                  上一步
                </Button>
              )}
              {activeStep === steps.length - 1 ? (
                <Button colorScheme="blue" type="submit">
                  提交
                </Button>
              ) : (
                <Button
                  colorScheme="blue"
                  type="button"
                  isDisabled={!image}
                  onClick={goToNext}
                >
                  下一步
                </Button>
              )}
            </ModalFooter>
          </form>
        </ModalContent>
      </Modal>
    </>
  );
}
