namespace MinervaFoods.Api.Features.Estados.Common
{
    /// <summary>
    /// API response model for Projects operation
    /// </summary>
    public class EstadoResponse
    {
        public Guid Id { get; set; }
        public Guid PaisId { get;  set; }
        public string Sigla { get;  set; } = string.Empty;
        public string Nome { get;  set; } = string.Empty;
    }
}
