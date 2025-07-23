using MinervaFoods.Api.Features.PedidosItens.PedidoItemCreate;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Api.Features.Pedidos.PedidoCreate
{
    /// <summary>
    /// Represents a request to create a new carne in the system.
    /// </summary>
    public class PedidoCreateRequest
    {
        /// <summary>
        /// Identificador do comprador responsável pelo pedido.
        /// </summary>
        public Guid CompradorId { get; set; }

     
        /// <summary>
        /// Data de criação do pedido.
        /// </summary>
        public DateTime DataPedido { get; set; }


        /// <summary>
        /// Lista de itens que compõem o pedido.
        /// </summary>
        public ICollection<PedidoItemCreateRequest> PedidoItem { get; set; } = new List<PedidoItemCreateRequest>();

    }
}
