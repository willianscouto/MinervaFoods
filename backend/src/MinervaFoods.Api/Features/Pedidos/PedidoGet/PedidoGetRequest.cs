namespace MinervaFoods.Api.Features.Pedidos.PedidoGet
{
    /// <summary>
    /// Request model for getting a pedido by ID
    /// </summary>
    public class PedidoGetRequest
    {
        /// <summary>
        /// The unique identifier of the pedido to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
