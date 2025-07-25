"use client";

import React, { useEffect, useState } from "react";
import {
  Box,
  Button,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions,
  Stack,
  Divider,
  CircularProgress,
  IconButton,
} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import VisibilityIcon from "@mui/icons-material/Visibility";
import { useRouter } from "next/navigation";
import { Pedido } from "@/interfaces/Pedido";
import { PedidoItem } from "@/interfaces/PedidoItem";
import { pedidoService } from "@/services/pedidoService";
import { useToast } from "@/contexts/ToastContext";
import { StatusPedidoEnum } from "@/interfaces/enums/StatusPedidoEnum";
import { formatarDataExibicao, formatarMoeda } from "@/utils/converter";
import { MoedaEnum } from "@/interfaces/enums/MoedaEnum";

const headerCellStyle = {
  backgroundColor: "#E84752",
  color: "#fff",
  fontWeight: "bold",
};

export default function ListaPedidos() {
  const { openToast } = useToast();
  const [pedidos, setPedidos] = useState<Pedido[]>([]);
  const [itensPedido, setItensPedido] = useState<PedidoItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [openDeleteDialog, setOpenDeleteDialog] = useState(false);
  const [openItensDialog, setOpenItensDialog] = useState(false);
  const router = useRouter();

  useEffect(() => {
    fetchPedidos();
  }, []);

  async function fetchPedidos() {
    setLoading(true);
    try {
      const data = await pedidoService.getAll();
      setPedidos(data);
    } catch (error) {
      openToast({
        message: "Erro ao carregar pedidos: " + error,
        severity: "error",
      });
    } finally {
      setLoading(false);
    }
  }

  function handleEdit(id: string) {
    router.push(`/pedidos/editar/${id}`);
  }

  function handleViewItens(itens: PedidoItem[]) {
    setItensPedido(itens);
    setOpenItensDialog(true);
  }

  function handleDeleteClick(id: string) {
    setDeleteId(id);
    setOpenDeleteDialog(true);
  }

  async function handleDeleteConfirm() {
    if (!deleteId) return;
    try {
      await pedidoService.delete(deleteId);
      setOpenDeleteDialog(false);
      setDeleteId(null);
      fetchPedidos();
      openToast({
        message: "Pedido deletado com sucesso!",
        severity: "success",
      });
    } catch (error) {
      openToast({
        message: "Erro ao deletar pedido: " + error,
        severity: "error",
      });
      setOpenDeleteDialog(false);
    }
  }

  return (
    <Box p={2}>
      <Stack
        direction="row"
        justifyContent="space-between"
        alignItems="center"
        mb={1}
      >
        <Typography variant="h4">Pedidos</Typography>
        <Button
          variant="contained"
          color="primary"
          size="large"
          sx={{ minWidth: 140 }}
          onClick={() => router.push("/pedidos/cadastro")}
        >
          Novo Pedido
        </Button>
      </Stack>

      <Divider sx={{ mb: 4 }} />

      {loading ? (
        <Box
          display="flex"
          justifyContent="center"
          alignItems="center"
          height="200px"
        >
          <CircularProgress />
        </Box>
      ) : (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell sx={headerCellStyle}>Ações</TableCell>
                <TableCell sx={headerCellStyle}>Número</TableCell>
                <TableCell sx={headerCellStyle}>Comprador</TableCell>
                <TableCell sx={headerCellStyle}>Data</TableCell>
                <TableCell sx={headerCellStyle}>Status</TableCell>
                <TableCell sx={headerCellStyle}>Total</TableCell>
                <TableCell sx={headerCellStyle}>Itens</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {pedidos.map((pedido) => (
                <TableRow key={pedido.id}>
                  <TableCell>
                    <Stack direction="row" spacing={1}>
                      <IconButton
                        size="small"
                        color="primary"
                        onClick={() => handleEdit(pedido.id!)}
                      >
                        <EditIcon />
                      </IconButton>
                      <IconButton
                        size="small"
                        color="error"
                        onClick={() => handleDeleteClick(pedido.id!)}
                      >
                        <DeleteIcon />
                      </IconButton>
                    </Stack>
                  </TableCell>
                  <TableCell>{pedido.numeroPedido || "N/A"}</TableCell>
                  <TableCell>{pedido.comprador?.nome || "N/A"}</TableCell>
                  <TableCell>
                    {formatarDataExibicao(pedido.dataPedido)}
                  </TableCell>
                  <TableCell>
                    {StatusPedidoEnum[pedido.statusPedido!]}
                  </TableCell>
                 <TableCell>{formatarMoeda(pedido.valorTotal)}</TableCell>
                  <TableCell>
                    <IconButton
                      size="small"
                      color="info"
                      onClick={() => handleViewItens(pedido.pedidoItem)}
                    >
                      <VisibilityIcon />
                    </IconButton>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}

      {/* Modal de confirmação de exclusão */}
      <Dialog
        open={openDeleteDialog}
        onClose={() => setOpenDeleteDialog(false)}
      >
        <DialogTitle>Confirmar Exclusão</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Tem certeza que deseja excluir este pedido? Esta ação não pode ser
            desfeita.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpenDeleteDialog(false)}>Cancelar</Button>
          <Button color="error" onClick={handleDeleteConfirm}>
            Excluir
          </Button>
        </DialogActions>
      </Dialog>

      {/* Modal de visualização de itens */}
      <Dialog
        open={openItensDialog}
        onClose={() => setOpenItensDialog(false)}
        maxWidth="md"
        fullWidth
      >
        <DialogTitle>Itens do Pedido</DialogTitle>
        <DialogContent>
          <TableContainer>
            <Table>
              <TableHead>
                <TableRow>
                  <TableCell sx={headerCellStyle}>Carne</TableCell>
                  <TableCell sx={headerCellStyle}>Quantidade</TableCell>
                  <TableCell sx={headerCellStyle}>Preço Unitário</TableCell>
                  <TableCell sx={headerCellStyle}>Moeda</TableCell>
                  <TableCell sx={headerCellStyle}>Cotação</TableCell>
                  <TableCell sx={headerCellStyle}>Total</TableCell>
                  <TableCell sx={headerCellStyle}>
                    Valor Total Cotação R$
                  </TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {itensPedido.map((item, index) => (
                  <TableRow key={index}>
                    <TableCell>{item.carne?.nome || item.carneId}</TableCell>
                    <TableCell>{item.quantidade}</TableCell>
                    <TableCell>{item.precoUnitario}</TableCell>
                    <TableCell>{MoedaEnum[item.moeda!]}</TableCell>
                    <TableCell>{item.cotacao}</TableCell>
                    <TableCell>{item.total}</TableCell>
                    <TableCell>
                      {formatarMoeda(item.valorTotalCotacao)}
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpenItensDialog(false)}>Fechar</Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}
