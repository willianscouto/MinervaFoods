"use client";

import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import {
  Button,
  Typography,
  Box,
  Stack,
  CircularProgress,
} from "@mui/material";
import { Carne } from "@/interfaces/Carne";
import { carneService } from "@/services/carneService";
import FormInput from "@/components/forms/FormInput";
import SelectInput from "@/components/forms/SelectInput";
import { useRouter, useParams } from "next/navigation";
import { tipoCarneOptions } from "@/interfaces/enums/TipoCarneEnum";
import { yupResolver } from "@hookform/resolvers/yup";
import { carneValidation } from "@/validation/carneValidation";
import { useToast } from "@/contexts/ToastContext";

export default function EditarCarnePage() {
  const { openToast } = useToast();
  const router = useRouter();
  const params = useParams();
  const rawId = params?.id;
  const id = Array.isArray(rawId) ? rawId[0] : rawId;

  const [loading, setLoading] = useState(true);

  const {
    handleSubmit,
    control,
    reset,
    formState: { errors },
  } = useForm<Omit<Carne, "id">>({
    resolver: yupResolver(carneValidation),
  });

  useEffect(() => {
    async function fetchCarne() {
      if (!id) {
        openToast({ message: "ID da carne não fornecido.", severity: "error" });
        return;
      }

      setLoading(true);
      try {
        const carne = await carneService.getById(id);
        reset({
          ean: carne.ean,
          nome: carne.nome,
          unidadeMedida: carne.unidadeMedida,
          tipoCarne: carne.tipoCarne,
        });
      } catch (error) {
        openToast({
          message: "Erro ao carregar carne: " + error,
          severity: "error",
        });
      } finally {
        setLoading(false);
      }
    }

    fetchCarne();
  }, [id, reset, openToast]);

  const onSubmit = async (data: Omit<Carne, "id">) => {
    if (!id) {
      openToast({ message: "ID da carne não fornecido.", severity: "error" });
      return;
    }

    try {
    await carneService.update({ ...data, id });
      openToast({
        message: "Carne atualizada com sucesso!",
        severity: "success",
      });
      router.push("/carnes");
    } catch (error) {
      openToast({
        message: "Erro ao atualizar carne: " + error,
        severity: "error",
      });
    }
  };

  if (loading) {
    return (
      <Box
        display="flex"
        justifyContent="center"
        alignItems="center"
        height="200px"
      >
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate>
      <Typography variant="h4" gutterBottom>
        Editar Carne
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
        options={tipoCarneOptions} // certifique-se que value é number aqui
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
