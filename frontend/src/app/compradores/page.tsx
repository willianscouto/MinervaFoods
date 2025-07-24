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
import { useRouter } from "next/navigation";
import { Comprador } from "@/interfaces/Comprador";
import { compradorService } from "@/services/compradorService";
import { useToast } from "@/contexts/ToastContext";

const headerCellStyle = {
  backgroundColor: "#E3000F",
  color: "#fff",
  fontWeight: "bold",
};

export default function Compradores() {
  const { openToast } = useToast();
  const [compradores, setCompradores] = useState<Comprador[]>([]);
  const [loading, setLoading] = useState(true);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [openDialog, setOpenDialog] = useState(false);
  const router = useRouter();

  useEffect(() => {
    fetchCompradores();
  }, []);

  async function fetchCompradores() {
    setLoading(true);
    try {
      const data = await compradorService.getAll();
      setCompradores(data);
    } catch (error) {
      openToast({ message: "Erro ao carregar compradores: " + error, severity: "error" });
    } finally {
      setLoading(false);
    }
  }

  function handleEdit(id: string) {
    router.push(`/compradores/editar/${id}`);
  }

  function handleDeleteClick(id: string) {
    setDeleteId(id);
    setOpenDialog(true);
  }

  async function handleDeleteConfirm() {
    if (!deleteId) return;
    try {
      await compradorService.delete(deleteId);
      setOpenDialog(false);
      setDeleteId(null);
      fetchCompradores();
      openToast({
        message: "Comprador deletado com sucesso!",
        severity: "success",
      });
    } catch (error) {
      openToast({
        message: "Erro ao deletar comprador: " + error,
        severity: "error",
      });
      setOpenDialog(false);
    }
  }

  return (
    <Box p={2}>
      <Stack direction="row" justifyContent="space-between" alignItems="center" mb={1}>
        <Typography variant="h4">Compradores</Typography>
        <Button
          variant="contained"
          color="primary"
          size="large"
          sx={{ minWidth: 140 }}
          onClick={() => router.push("/compradores/cadastro")}
        >
          Novo
        </Button>
      </Stack>

      <Divider sx={{ mb: 10 }} />

      {loading ? (
        <Box display="flex" justifyContent="center" alignItems="center" height="200px">
          <CircularProgress />
        </Box>
      ) : (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell sx={headerCellStyle}>Ações</TableCell>
                <TableCell sx={headerCellStyle}>Nome</TableCell>
                <TableCell sx={headerCellStyle}>Documento</TableCell>
                <TableCell sx={headerCellStyle}>Email</TableCell>
                <TableCell sx={headerCellStyle}>Data Nascimento</TableCell>
                <TableCell sx={headerCellStyle}>País</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {compradores.map((c) => (
                <TableRow key={c.id}>
                  <TableCell>
                    <Stack direction="row" spacing={1}>
                      <IconButton size="small" color="primary" aria-label="editar" onClick={() => handleEdit(c.id)}>
                        <EditIcon />
                      </IconButton>
                      <IconButton size="small" color="error" aria-label="excluir" onClick={() => handleDeleteClick(c.id)}>
                        <DeleteIcon />
                      </IconButton>
                    </Stack>
                  </TableCell>
                  <TableCell>{c.nome}</TableCell>
                  <TableCell>{c.documento}</TableCell>
                  <TableCell>{c.email}</TableCell>
                  <TableCell>{c.dataNascimento ? new Date(c.dataNascimento).toLocaleDateString("pt-BR") : "-"}</TableCell>
                  <TableCell>{c.pais || "-"}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}

      {/* Modal de confirmação */}
      <Dialog open={openDialog} onClose={() => setOpenDialog(false)}>
        <DialogTitle>Confirmar Exclusão</DialogTitle>
        <DialogContent>
          <DialogContentText>Tem certeza que deseja excluir este comprador? Esta ação não pode ser desfeita.</DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpenDialog(false)}>Cancelar</Button>
          <Button color="error" onClick={handleDeleteConfirm}>
            Excluir
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}
