namespace MinervaFoods.Api.Features.Carnes.CarneDelete
{
    /// <summary>
    /// Request model for getting a carne by ID
    /// </summary>
    public class CarneDeleteRequest
    {
        /// <summary>
        /// The unique identifier of the carne to delete
        /// </summary>
        public Guid Id { get; set; }
    }
}
