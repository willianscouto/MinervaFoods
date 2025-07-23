namespace MinervaFoods.Api.Features.Carnes.CarneGet
{
    /// <summary>
    /// Request model for getting a carne by ID
    /// </summary>
    public class CarneGetRequest
    {
        /// <summary>
        /// The unique identifier of the carne to retrieve
        /// </summary>
        public Guid Id { get; set; }
    }
}
