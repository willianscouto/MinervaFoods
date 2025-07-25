"use client";

import { useForm } from "react-hook-form";
import { Button, Typography, Box, Stack } from "@mui/material";
import { Carne } from "@/interfaces/Carne";
import { carneService } from "@/services/carneService";
import FormInput from "@/components/forms/ControllerFormInput";
import SelectInput from "@/components/forms/ControllerSelectInput";
import { useRouter } from "next/navigation";
import { tipoCarneOptions } from "@/interfaces/enums/TipoCarneEnum";
import { yupResolver } from "@hookform/resolvers/yup";
import { carneValidation } from "@/validation/carneValidation";
import { useToast } from "@/contexts/ToastContext";

export default function CadastroCarne() {
  const {
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<Omit<Carne, "id">>({
    resolver: yupResolver(carneValidation),
    defaultValues: {
      unidadeMedida: "KG",
    },
  });
  const { openToast } = useToast();
  const router = useRouter();

  const onSubmit = async (data: Omit<Carne, "id">) => {
    try {
      await carneService.create(data);
      openToast({
        message: "Carne cadastrada com sucesso!",
        severity: "success",
      });
      router.push("/carnes");
    } catch (error) {
      console.error("Erro ao cadastrar carne:", error);
      openToast({ message: "Erro ao cadastrar carne:", severity: "error" });
    }
  };

  return (
    <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate>
      <Stack
        direction="row"
        justifyContent="space-between"
        alignItems="center"
        mb={2}
      >
        <Typography variant="h4" gutterBottom>
          Cadastro de Carne
        </Typography>
        <Button    
        variant="outlined"
          color="secondary"
          size="large"
          sx={{ minWidth: 140 }} onClick={() => router.back()}>
          Voltar
        </Button>
      </Stack>

      <FormInput
        name="ean"
        label="EAN"
        control={control}
        required
        maxLength={13}
      />
      <FormInput
        name="nome"
        label="Nome"
        control={control}
        required
        maxLength={90}
      />
       <FormInput
        name="unidadeMedida"
        label="Unidade Medida"
        control={control}
        required
        maxLength={5}
      />
      <SelectInput
        name="tipoCarne"
        label="Tipo"
        control={control}
        required
        options={tipoCarneOptions}
      />

      <Button type="submit" variant="contained" color="primary"  size="large"
          sx={{ minWidth: 140 }}>
        Salvar
      </Button>
    </Box>
  );
}
