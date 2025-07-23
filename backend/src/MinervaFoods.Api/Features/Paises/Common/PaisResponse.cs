namespace MinervaFoods.Api.Features.Paises.Common
{
    /// <summary>
    /// API response model for Projects operation
    /// </summary>
    public class PaisResponse
    {
    
        public Guid Id { get; set; }
        public string Sigla { get; set; } = string.Empty;
        public string Nome { get;  set; } = string.Empty;
    }
}
