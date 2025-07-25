"use client";

import React, { useEffect, useState } from "react";
import { useForm, Controller } from "react-hook-form";
import {
  Box,
  Button,
  Typography,
  Stack,
  TextField,
  CircularProgress,
} from "@mui/material";
import { useRouter, useParams } from "next/navigation";
import { useToast } from "@/contexts/ToastContext";
import { compradorService } from "@/services/compradorService";
import { cepService } from "@/services/cepService";
import { paisService } from "@/services/paisService";
import { estadoService } from "@/services/estadoService";
import { Comprador } from "@/interfaces/Comprador";
import { Pais } from "@/interfaces/Pais";
import { Estado } from "@/interfaces/Estado";
import SelectInput from "@/components/forms/ControllerSelectInput";
import {formatarDataHoraInput} from "@/utils/converter";

type FormData = Omit<Comprador, "id">;

export default function EditarComprador() {
  const { openToast } = useToast();
  const router = useRouter();
  const params = useParams();
  const compradorId = params?.id as string;

  const [loading, setLoading] = useState(true);
  const [loadingCep, setLoadingCep] = useState(false);
  const [cepValido, setCepValido] = useState(false);
  const [paises, setPaises] = useState<Pais[]>([]);
  const [estados, setEstados] = useState<Estado[]>([]);



  const {
    handleSubmit,
    control,
    watch,
    setValue,
    getValues,
    reset,
    formState: { errors },
  } = useForm<FormData>();

  const cep = watch("cep");
  const paisSelecionado = watch("pais");


  // Carregar comprador existente
  useEffect(() => {
    if (!compradorId) return;
    compradorService
      .getById(compradorId)
      .then((data) => {
        const [logradouro, numero] = data.logradouro?.split(", ") ?? ["", ""];
        reset({ ...data, logradouro, numero });
      })
      .catch(() =>
        openToast({ message: "Erro ao carregar comprador", severity: "error" })
      )
      .finally(() => setLoading(false));
  }, [compradorId]);

  // Carrega países
  useEffect(() => {
    paisService
      .getAll()
      .then(setPaises)
      .catch(() =>
        openToast({ message: "Erro ao carregar países", severity: "error" })
      );
  }, []);

  // Estados por país
  useEffect(() => {
    estadoService
      .getAll()
      .then((todos) => {
        if (paisSelecionado) {
          const paisSelect = paises.find((e) => e.sigla === paisSelecionado);
          const filtrados = todos.filter((e) => e.paisId === paisSelect?.id);
          setEstados(filtrados);
        } else {
          setEstados(todos);
        }
      })
      .catch(() =>
        openToast({ message: "Erro ao carregar estados", severity: "error" })
      );
  }, [paisSelecionado, paises]);

  // CEP auto preenchimento
  const handleConsultarCep = async () => {
    const cepLimpo = cep?.replace(/\D/g, "");
    if (!cepLimpo || cepLimpo.length !== 8) return;
    setLoadingCep(true);
    try {
      const response = await cepService.getCep(cepLimpo);
      if (response.data.erro) {
        setCepValido(false);
        openToast({
          message: "CEP não encontrado. Preencha manualmente.",
          severity: "warning",
        });
      } else {
        const { logradouro, bairro, localidade, uf } = response.data;
        setValue("logradouro", logradouro || "");
        setValue("bairro", bairro || "");
        setValue("cidade", localidade || "");
        setValue("pais", "BR");
        setValue("estado", uf || "");
        setCepValido(true);
      }
    } catch (error) {
      setCepValido(false);
      openToast({ message: "Erro ao consultar CEP", severity: "error" });
    } finally {
      setLoadingCep(false);
    }
  };

  // Ajusta estadoId pelo UF
  useEffect(() => {
    const uf = getValues("estado");
    if (!uf || estados.length === 0) return;
    let estadoEncontrado = estados.find((e) => e.sigla === uf);
    if (estadoEncontrado) {
      setValue("estado", estadoEncontrado.sigla);
    }
    else {

     estadoEncontrado = estados.find((e) => e.id === uf);
    setValue("estado", estadoEncontrado?.sigla);
    }
  }, [estados]);

  const onSubmit = async (data: FormData) => {
    try {
      const enderecoCompleto = `${data.logradouro}, ${data.numero}`;
      const payload = {
        ...data,
        logradouro: enderecoCompleto,
        id: compradorId
      };

      await compradorService.update(payload);
      openToast({
        message: "Comprador atualizado com sucesso!",
        severity: "success",
      });
      router.push("/compradores");
    } catch (error) {
      openToast({
        message: "Erro ao atualizar comprador: " + error,
        severity: "error",
      });
    }
  };

  if (loading) return <CircularProgress />;

  return (
    <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate>
      <Typography variant="h4" gutterBottom>
        Editar Comprador
      </Typography>

      <Stack spacing={2}>
        {/* Nome */}
        <Controller
          name="nome"
          control={control}
          rules={{
            required: "Nome é obrigatório",
            maxLength: { value: 200, message: "Máximo 200 caracteres" },
          }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Nome"
              error={!!errors.nome}
              helperText={errors.nome?.message}
              fullWidth
              required
            />
          )}
        />

        {/* Documento */}
        <Controller
          name="documento"
          control={control}
          rules={{
            required: "Documento é obrigatório",
            maxLength: { value: 20, message: "Máximo 20 caracteres" },
          }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Documento"
              error={!!errors.documento}
              helperText={errors.documento?.message}
              fullWidth
              required
            />
          )}
        />

        {/* Email */}
        <Controller
          name="email"
          control={control}
          rules={{
            required: "Email é obrigatório",
            pattern: {
              value: /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/,
              message: "Email inválido",
            },
          }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Email"
              error={!!errors.email}
              helperText={errors.email?.message}
              fullWidth
              required
            />
          )}
        />

        {/* Telefone */}
        <Controller
          name="telefone"
          control={control}
          rules={{
            maxLength: { value: 20, message: "Máximo 20 caracteres" },
          }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Telefone"
              error={!!errors.telefone}
              helperText={errors.telefone?.message}
              fullWidth
            />
          )}
        />

        {/* Data Nascimento */}
        <Controller
          name="dataNascimento"
          control={control}
          rules={{
            required: "Data de nascimento é obrigatória",
            validate: (value) => {
              if (!value) return true; // permitido vazio se opcional
              const data = new Date(value);
              if (isNaN(data.getTime())) return "Data inválida";
              if (data >= new Date()) return "Data deve ser anterior à hoje";
              return true;
            },
          }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Data de Nascimento"
              type="date"
              InputLabelProps={{ shrink: true }}
              error={!!errors.dataNascimento}
              helperText={errors.dataNascimento?.message}
                value={formatarDataHoraInput(field.value)}
              fullWidth
              required
            />
          )}
        />

        {/* CEP */}
        <Controller
          name="cep"
          control={control}
          rules={{
            required: "CEP é obrigatório",
            minLength: {
              value: 3,
              message: "CEP deve ter ao menos 3 caracteres",
            },
          }}
          render={({ field }) => (
            <TextField
              {...field}
              label="CEP"
              error={!!errors.cep}
              helperText={errors.cep?.message}
              fullWidth
              disabled={loadingCep}
              required
              onBlur={handleConsultarCep}
            />
          )}
        />
        {loadingCep && <CircularProgress size={24} />}

        <Controller
          name="logradouro"
          control={control}
          rules={{
            maxLength: { value: 200, message: "Máximo 200 caracteres" },
            required:
              !cepValido &&
              "Logradouro é obrigatório quando CEP não encontrado",
          }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Logradouro"
              error={!!errors.logradouro}
              helperText={errors.logradouro?.message}
              fullWidth
              disabled={cepValido}
              required={!cepValido}
            />
          )}
        />

        {/* Numero */}
        <Controller
          name="numero"
          control={control}
          rules={{ required: "Número é obrigatório" }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Número"
              error={!!errors.numero}
              helperText={errors.numero?.message}
              fullWidth
              required
            />
          )}
        />

        {/* Complemento */}
        <Controller
          name="complemento"
          control={control}
          rules={{
            maxLength: { value: 100, message: "Máximo 100 caracteres" },
          }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Complemento"
              error={!!errors.complemento}
              helperText={errors.complemento?.message}
              fullWidth
              disabled={!cepValido ? false : false} // sempre liberado
            />
          )}
        />

        {/* Bairro */}
        <Controller
          name="bairro"
          control={control}
          rules={{
            maxLength: { value: 100, message: "Máximo 100 caracteres" },
            required:
              !cepValido && "Bairro é obrigatório quando CEP não encontrado",
          }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Bairro"
              error={!!errors.bairro}
              helperText={errors.bairro?.message}
              fullWidth
              disabled={cepValido}
              required={!cepValido}
            />
          )}
        />

        {/* Cidade */}
        <Controller
          name="cidade"
          control={control}
          rules={{
            maxLength: { value: 100, message: "Máximo 100 caracteres" },
            required:
              !cepValido && "Cidade é obrigatória quando CEP não encontrado",
          }}
          render={({ field }) => (
            <TextField
              {...field}
              label="Cidade"
              error={!!errors.cidade}
              helperText={errors.cidade?.message}
              fullWidth
              disabled={cepValido}
              required={!cepValido}
            />
          )}
        />

        {/* País (select) */}
        <SelectInput
          name="pais"
          label="País"
          control={control}
          required
          options={paises.map((p) => ({ label: p.nome, value: p.sigla }))}
          disabled={cepValido}
        />

        {/* Estado (select dinâmico baseado no país) */}
        <SelectInput
          name="estado"
          label="Estado"
          control={control}
          required
          options={estados.map((e) => ({
            label: `${e.sigla} - ${e.nome}`,
            value: e.sigla,
          }))}
          disabled={estados.length === 0}
        />
      </Stack>

      <Stack direction="row" spacing={2} sx={{ mt: 3 }}>
        <Button
          variant="outlined"
          color="secondary"
          onClick={() => router.back()}
        >
          Voltar
        </Button>
        <Button type="submit" variant="contained" color="primary">
          Salvar
        </Button>
      </Stack>
    </Box>
  );
}
