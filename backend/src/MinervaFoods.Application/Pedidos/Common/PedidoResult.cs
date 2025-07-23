using MinervaFoods.Application.Compradores.Common;
using MinervaFoods.Application.PedidosItens.Common;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Application.Pedidos.Common
{
    /// <summary>
    /// Representa o resultado da operação com a entidade Pedido.
    /// </summary>
    public class PedidoResult
    {
        /// <summary>
        /// Identificador único da carne.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador do comprador que realizou o pedido.
        /// </summary>
        public Guid CompradorId { get; set; }

        /// <summary>
        /// Identificador do comprador que realizou o pedido.
        /// </summary>
        public CompradorResult Comprador { get; set; } = null!;

        /// <summary>
        /// Identificador do do numero do pedido.
        /// </summary>
        public long NumeroPedido { get; set; }

        /// <summary>
        /// Data em que o pedido foi realizado.
        /// </summary>
        public DateTime DataPedido { get; set; }

        /// <summary>
        /// Status atual do pedido (Aberto, Finalizado, Cancelado).
        /// </summary>
        public PedidoEnum.Status StatusPedido { get; set; }

        /// <summary>
        /// Coleção dos itens (carnes) que compõem o pedido.
        /// </summary>
        public ICollection<PedidoItemResult> PedidoItem { get; set; } = new List<PedidoItemResult>();

        /// <summary>
        /// Valor total do pedido calculado com base nos itens.
        /// </summary>
        public decimal ValorTotal { get;  }
    }
}
