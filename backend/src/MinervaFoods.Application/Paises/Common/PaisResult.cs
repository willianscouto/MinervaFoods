using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Application.Paises.Common
{
    /// <summary>
    /// Representa o resultado da operação com a entidade Pais.
    /// </summary>
    public class PaisResult
    {
        public Guid Id { get; set; }
        public string Sigla { get;  set; } = string.Empty;
        public string Nome { get;  set; } = string.Empty;
    }
}
