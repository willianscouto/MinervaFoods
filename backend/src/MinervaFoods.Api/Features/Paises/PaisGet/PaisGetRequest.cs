namespace MinervaFoods.Api.Features.Paises.PaisGet
{
    /// <summary>
    /// Request model for getting a paises by ID
    /// </summary>
    public class PaisGetRequest
    {
        /// <summary>
        /// The unique identifier of the paises to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
