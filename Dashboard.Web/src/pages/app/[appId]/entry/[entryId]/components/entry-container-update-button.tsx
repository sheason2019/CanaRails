import {
  Box,
  Button,
  Modal,
  ModalBody,
  ModalContent,
  ModalFooter,
  ModalHeader,
  ModalOverlay,
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
} from "@chakra-ui/react";

const steps = ["选择镜像", "填写配置", "提交变更"];

export default function EntryContainerUpdateButton() {
  const { isOpen, onClose, onOpen } = useDisclosure();

  const { activeStep } = useSteps({
    index: 0,
    count: 3,
  });

  return (
    <>
      <Button onClick={onOpen}>配置变更</Button>
      <Modal isOpen={isOpen} onClose={onClose} size="xl" isCentered>
        <ModalOverlay />
        <ModalContent>
          <ModalHeader>
            <Stepper index={activeStep}>
              {steps.map((step) => (
                <Step key={step}>
                  <StepIndicator>
                    <StepStatus
                      complete={<StepIcon />}
                      incomplete={<StepNumber />}
                      active={<StepNumber />}
                    />
                  </StepIndicator>
                  <Box flexShrink="0">
                    <StepTitle>{step}</StepTitle>
                  </Box>
                  <StepSeparator />
                </Step>
              ))}
            </Stepper>
          </ModalHeader>

          <ModalBody></ModalBody>

          <ModalFooter gap={2}>
            <Button>取消</Button>
            <Button colorScheme="blue">下一步</Button>
          </ModalFooter>
        </ModalContent>
      </Modal>
    </>
  );
}
