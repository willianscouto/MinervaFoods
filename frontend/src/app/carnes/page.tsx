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
import { Carne } from "@/interfaces/Carne";
import { carneService } from "@/services/carneService";
import { useToast } from "@/contexts/ToastContext";
import { TipoCarneEnum } from "@/interfaces/enums/TipoCarneEnum";

const headerCellStyle = {
  backgroundColor: "#E3000F",
  color: "#fff",
  fontWeight: "bold",
};

export default function ListaCarnes() {
  const { openToast } = useToast();
  const [carnes, setCarnes] = useState<Carne[]>([]);
  const [loading, setLoading] = useState(true);
  const [deleteId, setDeleteId] = useState<string | null>(null);
  const [openDialog, setOpenDialog] = useState(false);
  const router = useRouter();

  useEffect(() => {
    fetchCarnes();
  }, []);

  async function fetchCarnes() {
    setLoading(true);
    try {
      const data = await carneService.getAll();
      setCarnes(data);
    } catch (error) {
      openToast({ message: "Erro ao carregar carne." + error, severity: "error" });
    } finally {
      setLoading(false);
    }
  }

  function handleEdit(id: string) {
    router.push(`/carnes/editar/${id}`);
  }

  function handleDeleteClick(id: string) {
    setDeleteId(id);
    setOpenDialog(true);
  }

  async function handleDeleteConfirm() {
    if (!deleteId) return;
    try {
      await carneService.delete(deleteId);

      setOpenDialog(false);
      setDeleteId(null);
      fetchCarnes();
       openToast({
        message: "Carne deletada com sucesso!",
        severity: "success",
      });
    } catch (error) {
      openToast({
          message: "Erro ao deletar carne: " + error,
          severity: "error",
        });
      setOpenDialog(false);
    }
  }

  return (
    <Box p={2}>
      <Stack direction="row" justifyContent="space-between" alignItems="center" mb={1}>
        <Typography variant="h4">Carnes</Typography>
        <Button
          variant="contained"
          color="primary"
          size="large"
          sx={{ minWidth: 140 }}
          onClick={() => router.push("/carnes/cadastro")}
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
                <TableCell sx={headerCellStyle}>EAN</TableCell>
                <TableCell sx={headerCellStyle}>Tipo</TableCell>
                <TableCell sx={headerCellStyle}>Unidade Medida</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {carnes.map((carne) => (
                <TableRow key={carne.id}>
                  <TableCell>
                    <Stack direction="row" spacing={1}>
                      <IconButton
                        size="small"
                        color="primary"
                        aria-label="editar"
                        onClick={() => handleEdit(carne.id)}
                      >
                        <EditIcon />
                      </IconButton>

                      <IconButton
                        size="small"
                        color="error"
                        aria-label="excluir"
                        onClick={() => handleDeleteClick(carne.id)}
                      >
                        <DeleteIcon />
                      </IconButton>
                    </Stack>
                  </TableCell>
                  <TableCell>{carne.nome}</TableCell>
                  <TableCell>{carne.ean}</TableCell>
                  <TableCell>{TipoCarneEnum[carne.tipoCarne]}</TableCell>
                  <TableCell>{carne.unidadeMedida || "KG"}</TableCell>
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
          <DialogContentText>
            Tem certeza que deseja excluir esta carne? Esta ação não pode ser desfeita.
          </DialogContentText>
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
