namespace MinervaFoods.Api.Features.Compradores.CompradorGet
{
    /// <summary>
    /// Request model for getting a comprador by ID
    /// </summary>
    public class CompradorGetRequest
    {
        /// <summary>
        /// The unique identifier of the comprador to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
