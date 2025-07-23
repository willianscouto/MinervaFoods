namespace MinervaFoods.Api.Features.Pedidos.PedidoDelete
{
    /// <summary>
    /// Request model for getting a pedido by ID
    /// </summary>
    public class PedidoDeleteRequest
    {
        /// <summary>
        /// The unique identifier of the pedido to delete
        /// </summary>
        public Guid Id { get; set; }
    }
}
