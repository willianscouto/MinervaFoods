export enum StatusPedidoEnum {
  Unknown = 0,
  Aberto = 1,
  Finalizado = 2,
  Cancelado = 3,
}

export const situacaoPedidoOptions = [
  { label: 'Selecione a situação', value: StatusPedidoEnum.Unknown },
  { label: 'Aberto', value: StatusPedidoEnum.Aberto },
  { label: 'Finalizado', value: StatusPedidoEnum.Finalizado },
  { label: 'Cancelado', value: StatusPedidoEnum.Cancelado },
];
