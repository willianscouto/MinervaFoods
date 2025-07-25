'use client';

import { useEffect, useState } from "react";
import { useForm, SubmitHandler, useFieldArray } from "react-hook-form";
import { useRouter } from "next/navigation";
import {
  Button,
  Typography,
  Box,
  Stack,
  CircularProgress,
  Alert,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  IconButton,
  Divider,
  TextField
} from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";

import { compradorService } from "@/services/compradorService";
import { carneService } from "@/services/carneService";
import { pedidoService } from "@/services/pedidoService";

import { pedidoValidation } from "@/validation/pedidoValidation";

import ControllerFormInput from "@/components/forms/ControllerFormInput";
import ControllerSelectInput from "@/components/forms/ControllerSelectInput";
import SelectInput from "@/components/forms/SelectInput";
import { useToast } from "@/contexts/ToastContext";
import { yupResolver } from "@hookform/resolvers/yup";

import { Pedido } from "@/interfaces/Pedido";
import { PedidoItem } from "@/interfaces/PedidoItem";
import { StatusPedidoEnum } from "@/interfaces/enums/StatusPedidoEnum";
import { MoedaEnum, moedaOptions } from "@/interfaces/enums/MoedaEnum";
import { formatarDataHoraInput } from "@/utils/converter";
import { v4 as uuidv4 } from "uuid";

const headerCellStyle = {
  backgroundColor: "#E3000F",
  color: "#fff",
  fontWeight: "bold",
};

type Option = { value: string; label: string };

export default function CadastroPedido() {
  const { openToast } = useToast();
  const [temCompradores, setTemCompradores] = useState(false);
  const [temCarnes, setTemCarnes] = useState(false);
  const [compradoresOptions, setCompradoresOptions] = useState<Option[]>([]);
  const [carnesOptions, setCarnesOptions] = useState<Option[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const router = useRouter();

  // Estado local para novo item a ser adicionado
  const [novoItem, setNovoItem] = useState<PedidoItem>({
    id: "",
    carneId: "",
    quantidade: 1,
    moeda: MoedaEnum.Unknown,
    precoUnitario: 0,
  });

  const {
    control,
    handleSubmit,
    formState: { errors },
    getValues,
  } = useForm<Pedido>({
    resolver: yupResolver(pedidoValidation),
    defaultValues: {
      compradorId: "",
      dataPedido: formatarDataHoraInput(new Date()),
      pedidoItem: [],
      observacao: "",
      id: null,
      numeroPedido: null,
      comprador: null,
      statusPedido: StatusPedidoEnum.Aberto,
      valorTotal: null,
    },
  });

  const { fields, append, remove } = useFieldArray({
    control,
    name: "pedidoItem",
  });

  useEffect(() => {
    async function carregarDados() {
      try {
        const compradores = await compradorService.getAll();
        const carnes = await carneService.getAll();

        setTemCompradores(compradores.length > 0);
        setTemCarnes(carnes.length > 0);

        setCompradoresOptions(
          compradores.map((c) => ({ value: c.id, label: c.nome }))
        );
        setCarnesOptions(carnes.map((c) => ({ value: c.id, label: c.nome })));
      } catch {
        openToast({
          message: "Erro ao carregar compradores ou carnes",
          severity: "error",
        });
      } finally {
        setIsLoading(false);
      }
    }
    carregarDados();
  }, [openToast]);

  function adicionarItem() {
    if (
      !novoItem.carneId ||
      novoItem.quantidade <= 0 ||
      novoItem.moeda === MoedaEnum.Unknown ||
      novoItem.precoUnitario <= 0
    ) {
      openToast({
        message: "Preencha todos os campos corretamente",
        severity: "warning",
      });
      return;
    }

    const pedidoItems = getValues("pedidoItem");

    const jaExiste = pedidoItems.some(
      (item) =>
        item.carneId === novoItem.carneId && item.moeda === novoItem.moeda
    );
    if (jaExiste) {
      openToast({
        message: "Essa carne já foi adicionada nessa moeda",
        severity: "warning",
      });
      return;
    }

    append({
      ...novoItem,
      id: uuidv4(),
      total: novoItem.quantidade * novoItem.precoUnitario,
    });

    // Reset novo item
    setNovoItem({
      id: "",
      carneId: "",
      quantidade: 1,
      moeda: MoedaEnum.Unknown,
      precoUnitario: 0,
    });
  }

  const onSubmit: SubmitHandler<Pedido> = async (data) => {
     if(!data.compradorId) 
     openToast({ message: "Favor informar o compradodr", severity: "error" });
    
    try {
      await pedidoService.create(data);
        openToast({
        message: "Pedido cadastrado com sucesso!",
        severity: "success",
      });
      router.push('/pedidos');
    } catch (error) {
       
        openToast({ message: "Erro ao cadastrar pedido:" + error, severity: "error" });
    }
  };

  if (isLoading) return <CircularProgress />;

  if (!temCompradores || !temCarnes) {
    return (
      <Alert severity="warning" sx={{ mt: 2 }}>
        Para cadastrar um pedido, é necessário ter pelo menos um comprador e uma
        carne cadastrados.
      </Alert>
    );
  }

  return (
    <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate>
      <Stack
        direction="row"
        justifyContent="space-between"
        alignItems="center"
        mb={2}
      >
        <Typography variant="h4" gutterBottom>
          Cadastro de Pedido
        </Typography>
        <Button variant="outlined" onClick={() => router.back()}>
          Voltar
        </Button>
      </Stack>

      <Box sx={{ flex: 1, maxWidth: 700 }}>
        <Typography variant="h6" mb={1}>
          Dados Pedido
        </Typography>

        <ControllerFormInput
          name="dataPedido"
          label="Data Pedido"
          control={control}
          type="datetime-local"
          disabled
          required
        />

        <ControllerSelectInput
          name="compradorId"
          label="Comprador"
          control={control}
          required
          options={compradoresOptions}
        />

        <ControllerFormInput
          name="observacao"
          label="Observacao"
          control={control}
          multiline
          rows={4}
        />

        <Typography variant="h6" mb={1} sx={{ mt: 3 }}>
          Adicionar Carne
        </Typography>

        <SelectInput
          name="carneId"
          label="Carne"
          value={novoItem.carneId}
          onChange={(e) => setNovoItem({...novoItem, carneId: e.target.value})}
          required
          options={carnesOptions}
        />

        <SelectInput
          name="moeda"
          label="Moeda"
          value={novoItem.moeda}
          onChange={(e) => setNovoItem({...novoItem, moeda: e.target.value as MoedaEnum})}
          required
          options={moedaOptions}
        />

        <TextField
          fullWidth
          margin="normal"
          label="Quantidade"
          type="number"
          value={novoItem.quantidade}
          onChange={(e) => setNovoItem({...novoItem, quantidade: Number(e.target.value)})}
          required
          inputProps={{min: 1}}
        />

        <TextField
          fullWidth
          margin="normal"
          label="Preço Unitário"
          type="number"
          value={novoItem.precoUnitario}
          onChange={(e) => setNovoItem({...novoItem, precoUnitario: Number(e.target.value)})}
          required
          inputProps={{min: 0.01, step: 0.01}}
        />

        <Button
          variant="outlined"
          fullWidth
          sx={{ mt: 2 }}
          onClick={adicionarItem}
        >
          Adicionar Item
        </Button>
      </Box>

      <Divider sx={{ my: 5 }} />

      <Typography variant="h6" gutterBottom>
        Itens do Pedido
      </Typography>

      <TableContainer component={Paper}>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell sx={headerCellStyle}></TableCell>
              <TableCell sx={headerCellStyle}>Carne</TableCell>
              <TableCell sx={headerCellStyle}>Quantidade</TableCell>
              <TableCell sx={headerCellStyle}>Moeda</TableCell>
              <TableCell sx={headerCellStyle}>Preço Unitário</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {fields.map((item, index) => {
              const carne = carnesOptions.find((c) => c.value === item.carneId);
              return (
                <TableRow key={item.id}>
                  <TableCell>
                    <IconButton
                      onClick={() => remove(index)}
                      color="error"
                      size="small"
                    >
                      <DeleteIcon />
                    </IconButton>
                  </TableCell>
                  <TableCell>{carne?.label || item.carneId}</TableCell>
                  <TableCell>{item.quantidade}</TableCell>
                  <TableCell>{item.moeda}</TableCell>
                  <TableCell>{item.precoUnitario.toFixed(2)}</TableCell>
                </TableRow>
              );
            })}
          </TableBody>
        </Table>
      </TableContainer>

      <Button
        type="submit"
        variant="contained"
        color="primary"
        size="large"
        sx={{ mt: 4, minWidth: 140 }}
        disabled={Object.keys(errors).length > 0}
      >
        {Object.keys(errors).length > 0 ? "Corrija os erros" : "Salvar"}
      </Button>

      {Object.keys(errors).length > 0 && (
        <Alert severity="error" sx={{ mt: 2 }}>
          Existem {Object.keys(errors).length} erro(s) no formulário. Verifique
          os campos obrigatórios.
        </Alert>
      )}
    </Box>
  );
}