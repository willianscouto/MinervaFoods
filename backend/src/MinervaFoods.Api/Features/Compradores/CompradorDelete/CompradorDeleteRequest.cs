namespace MinervaFoods.Api.Features.Compradores.CompradorDelete
{
    /// <summary>
    /// Request model for getting a comprador by ID
    /// </summary>
    public class CompradorDeleteRequest
    {
        /// <summary>
        /// The unique identifier of the comprador to delete
        /// </summary>
        public Guid Id { get; set; }
    }
}
