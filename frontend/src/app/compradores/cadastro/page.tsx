'use client';

import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import {
  Box,
  Button,
  Typography,
  Stack,
  CircularProgress,
} from "@mui/material";
import { useRouter } from "next/navigation";
import { useToast } from "@/contexts/ToastContext";
import { compradorService } from "@/services/compradorService";
import { cepService } from "@/services/cepService";
import { paisService } from "@/services/paisService";
import { estadoService } from "@/services/estadoService";
import { Comprador } from "@/interfaces/Comprador";
import { Pais } from "@/interfaces/Pais";
import { Estado } from "@/interfaces/Estado";
import ControllerFormInput from "@/components/forms/ControllerFormInput";
import ControllerSelectInput from "@/components/forms/ControllerSelectInput";
import { yupResolver } from "@hookform/resolvers/yup";
import { compradorValidation } from "@/validation/compradorValidation";

export default function CadastroComprador() {
  const { openToast } = useToast();
  const router = useRouter();

  const [loadingCep, setLoadingCep] = useState(false);
  const [paises, setPaises] = useState<Pais[]>([]);
  const [estados, setEstados] = useState<Estado[]>([]);

  const {
    handleSubmit,
    control,
    watch,
    setValue,
    formState: { errors, isSubmitting },
  } = useForm<Comprador>({
    resolver: yupResolver(compradorValidation),
    defaultValues: {
      nome: "",
      documento: "",
      email: "",
      telefone: "",
      dataNascimento: null,
      cep: "",
      logradouro: "",
      numero: "",
      complemento: "",
      bairro: "",
      cidade: "",
      estado: "",
      pais: "",
    },
  });

  const cep = watch("cep");
  const paisSelecionado = watch("pais");

  // Carrega países no início
  useEffect(() => {
    paisService
      .getAll()
      .then(setPaises)
      .catch(() =>
        openToast({ message: "Erro ao carregar países", severity: "error" })
      );
  }, []);

  // Quando o país mudar, busca os estados correspondentes
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
  }, [paisSelecionado]);

  const handleConsultarCep = async () => {
    const cepLimpo = cep?.replace(/\D/g, "");
    if (!cepLimpo || cepLimpo.length !== 8) return;

    setLoadingCep(true);

    try {
      const response = await cepService.getCep(cepLimpo);
      if (response.data.erro) {
        openToast({
          message: "CEP não encontrado. Preencha os campos manualmente.",
          severity: "warning",
        });
      } else {
        const { logradouro, bairro, localidade, uf } = response.data;
        setValue("logradouro", logradouro || "");
        setValue("bairro", bairro || "");
        setValue("cidade", localidade || "");
        setValue("pais", "BR");
        setValue("estado", uf || "");
      }
    } catch (error) {
      openToast({
        message: "Erro ao consultar CEP: " + error,
        severity: "error",
      });
    } finally {
      setLoadingCep(false);
    }
  };

  const onSubmit = async (data: Comprador) => {
    try {
      const enderecoCompleto = `${data.logradouro}, ${data.numero}`;
      const payload = {
        ...data,
        logradouro: enderecoCompleto,
      };

      await compradorService.create(payload);

      openToast({
        message: "Comprador cadastrado com sucesso!",
        severity: "success",
      });
      router.push("/compradores");
    } catch (error) {
      openToast({
        message: "Erro ao cadastrar comprador: " + error,
        severity: "error",
      });
    }
  };

  return (
    <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate>
      <Stack
        direction="row"
        justifyContent="space-between"
        alignItems="center"
        mb={1}
      >
        <Typography variant="h4">
          Cadastro de Comprador
        </Typography>
        <Button    
          variant="outlined"
          color="secondary"
          size="large"
          sx={{ minWidth: 140 }} 
          onClick={() => router.back()}
        >
          Voltar
        </Button>
      </Stack>

      <Stack spacing={2}>
        <ControllerFormInput
          name="nome"
          label="Nome"
          control={control}
          required
        />

        <ControllerFormInput
          name="documento"
          label="Documento"
          control={control}
          required
        />

        <ControllerFormInput
          name="email"
          label="Email"
          control={control}
          required
        />

        <ControllerFormInput
          name="telefone"
          label="Telefone"
          control={control}
        />

        <ControllerFormInput
          name="dataNascimento"
          label="Data de Nascimento"
          control={control}
          type="date"
      
          InputLabelProps={{ shrink: true }}
        />

        <ControllerFormInput
          name="cep"
          label="CEP"
          control={control}

          onBlur={handleConsultarCep}
          disabled={loadingCep}
          InputProps={{
            endAdornment: loadingCep ? <CircularProgress size={24} /> : null,
          }}
        />

        <ControllerFormInput
          name="logradouro"
          label="Logradouro"
          control={control}
        
        />

        <ControllerFormInput
          name="numero"
          label="Número"
          control={control}
   
        />

        <ControllerFormInput
          name="complemento"
          label="Complemento"
          control={control}
        />

        <ControllerFormInput
          name="bairro"
          label="Bairro"
          control={control}
         
        />

        <ControllerFormInput
          name="cidade"
          label="Cidade"
          control={control}
       
        />

        <ControllerSelectInput
          name="pais"
          label="País"
          control={control}
          required
          options={paises.map((p) => ({ label: p.nome, value: p.sigla }))}
        />

        <ControllerSelectInput
          name="estado"
          label="Estado"
          control={control}
          options={estados.map((e) => ({
            label: `${e.sigla} - ${e.nome}`,
            value: e.sigla,
          }))}
          disabled={estados.length === 0}
        />
      </Stack>

      <Button
        type="submit"
        variant="contained"
        color="primary"
        size="large"
        sx={{ mt: 3, minWidth: 140 }}
        disabled={isSubmitting}
      >
        {isSubmitting ? <CircularProgress size={24} /> : "Salvar"}
      </Button>
    </Box>
  );
}