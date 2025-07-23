using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Application.Carnes.Common
{
    /// <summary>
    /// Representa o resultado da operação com a entidade Carne.
    /// </summary>
    public class CarneResult
    {
        /// <summary>
        /// Identificador único da carne.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Código internacional do produto (EAN).
        /// </summary>
        public string Ean { get; set; } = string.Empty;

        /// <summary>
        /// Nome da carne (ex: Picanha, Costela Suína).
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Tipo da carne (Bovina, Suína, Frango, etc).
        /// </summary>
        public CarneEnum.TipoCarne TipoCarne { get; set; }

        /// <summary>
        /// Unidade de medida (sempre KG por padrão).
        /// </summary>
        public string UnidadeMedida { get; set; } = "KG";
    }
}
