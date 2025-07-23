namespace MinervaFoods.Api.Features.Estados.EstadoGet
{
    /// <summary>
    /// Request model for getting a Estado by ID
    /// </summary>
    public class EstadoGetRequest
    {
        /// <summary>
        /// The unique identifier of the project to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
