using MinervaFoods.Api.Features.Compradores.Common;
using MinervaFoods.Api.Features.PedidosItens.Common;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Api.Features.Pedidos.Common
{
    /// <summary>
    /// API response model for Pedido operation
    /// </summary>
    public class PedidoResponse
    {
        /// <summary>
        /// The unique identifier of the project
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador do comprador que realizou o pedido.
        /// </summary>
        public Guid CompradorId { get;  set; }

        /// <summary>
        /// Identificador do comprador que realizou o pedido.
        /// </summary>
        public CompradorResponse Comprador { get; set; } = null!;

        /// <summary>
        /// Identificador do do numero do pedido.
        /// </summary>
        public long NumeroPedido { get;  set; }

        /// <summary>
        /// Data em que o pedido foi realizado.
        /// </summary>
        public DateTime DataPedido { get;  set; }

        /// <summary>
        /// Status atual do pedido (Aberto, Finalizado, Cancelado).
        /// </summary>
        public PedidoEnum.Status StatusPedido { get;  set; }

        /// <summary>
        /// Coleção dos itens (carnes) que compõem o pedido.
        /// </summary>
        public ICollection<PedidoItemResponse> PedidoItem { get; set; } = new List<PedidoItemResponse>();


        /// <summary>
        /// Valor total do pedido calculado com base nos itens.
        /// </summary>
        public decimal ValorTotal { get; set; }

    }
}
