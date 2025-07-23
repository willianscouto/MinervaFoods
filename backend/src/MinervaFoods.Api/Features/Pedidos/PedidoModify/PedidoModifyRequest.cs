using MinervaFoods.Api.Features.PedidosItens.PedidoItemModify;
using MinervaFoods.Application.PedidosItens.PedidoItemModify;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Api.Features.Pedidos.PedidoModify
{
    /// <summary>
    /// Represents a request to modify an existing pedido in the system.
    /// </summary>
    public class PedidoModifyRequest
    {
        /// <summary>
        /// The unique identifier of the project
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador do comprador responsável pelo pedido.
        /// </summary>
        public Guid CompradorId { get; set; }

        /// <summary>
        /// Número do pedido.
        /// </summary>
        public long NumeroPedido { get; set; }

        /// <summary>
        /// Data de criação do pedido.
        /// </summary>
        public DateTime DataPedido { get; set; }

        /// <summary>
        /// Status atual do pedido (por exemplo: Aberto, Finalizado, Cancelado).
        /// </summary>
        public PedidoEnum.Status StatusPedido { get; set; }

        /// <summary>
        /// Lista de itens que compõem o pedido.
        /// </summary>
        public ICollection<PedidoItemModifyRequest> PedidoItem { get; set; } = new List<PedidoItemModifyRequest>();

    }
}
