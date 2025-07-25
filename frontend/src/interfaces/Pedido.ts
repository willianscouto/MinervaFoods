import { PedidoItem } from './PedidoItem';
import { Comprador } from './Comprador';
import { StatusPedidoEnum } from './enums/StatusPedidoEnum';


export interface Pedido {
  id?: string| null;
  numeroPedido: string | null;
  dataPedido: string | null; 
  compradorId: string; 
  comprador?: Comprador | null;
  pedidoItem: PedidoItem[];
  statusPedido?: StatusPedidoEnum;
  valorTotal: number | null;
  observacao: string | null;
}