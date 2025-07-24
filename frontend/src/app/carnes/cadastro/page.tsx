"use client";

import { useForm } from "react-hook-form";
import { Button, Typography, Box, Stack } from "@mui/material";
import { Carne } from "@/interfaces/Carne";
import { carneService } from "@/services/carneService";
import FormInput from "@/components/forms/FormInput";
import SelectInput from "@/components/forms/SelectInput";
import { useRouter } from "next/navigation";
import { tipoCarneOptions } from "@/interfaces/enums/TipoCarneEnum";
import { yupResolver } from "@hookform/resolvers/yup";
import { carneValidation } from "@/validation/carneValidation";
import { useToast } from "@/contexts/ToastContext";

export default function CadastroCarne() {
  const { openToast } = useToast();
  const {
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<Omit<Carne, "id">>({
    resolver: yupResolver(carneValidation),
    defaultValues: {
      unidadeMedida: "KG",
      tipoCarne:0

    },
  });

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
      openToast({
        message: "Erro ao cadastrar carne." + error,
        severity: "error",
      });
    }
  };

  return (
    <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate>
      <Typography variant="h4" gutterBottom>
        Cadastro de Carne
      </Typography>

      <FormInput
        name="ean"
        label="EAN"
        control={control}
        type="number"
        maxLength={13}
        required
      />
      <FormInput
        name="nome"
        label="Nome"
        control={control}
        maxLength={70}
        required
      />
      <FormInput
        name="unidadeMedida"
        label="Unidade de Medida"
        control={control}
        maxLength={5}
        required
      />
      <SelectInput
        name="tipoCarne"
        label="Tipo"
        control={control}
        required
        options={tipoCarneOptions}
      />

      <Stack direction="row" spacing={2} sx={{ mt: 2 }}>
        <Button
          variant="outlined"
          color="secondary"
          size="large"
          sx={{ minWidth: 140 }}
          onClick={() => router.back()}
        >
          Voltar
        </Button>

        <Button
          type="submit"
          variant="contained"
          color="primary"
          size="large"
          sx={{ minWidth: 140 }}
        >
          Salvar
        </Button>
      </Stack>
    </Box>
  );
}
