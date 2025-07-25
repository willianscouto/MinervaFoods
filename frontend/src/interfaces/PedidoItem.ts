import { Carne } from './Carne';
import { MoedaEnum } from './enums/MoedaEnum';

export interface PedidoItem {
  id?: string; // Tornar opcional para criação
  pedidoId?: string; // Tornar opcional para criação
  carneId: string;
  carne?: Carne | null;
  quantidade: number;
  moeda: MoedaEnum;
  precoUnitario: number;
  total?: number | null;
  cotacao?: number | null;
  valorTotalCotacao?: number | null;
}