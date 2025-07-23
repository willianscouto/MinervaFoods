using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Api.Features.Carnes.CarneCreate
{
    /// <summary>
    /// Represents a request to create a new carne in the system.
    /// </summary>
    public class CarneCreateRequest
    {
        /// <summary>
        /// Código internacional do produto (EAN).
        /// </summary>
        public string Ean { get; set; } = string.Empty;

        /// <summary>
        /// Nome da carne (ex: Picanha, Costela Suína).
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Tipo da carne (ex: Bovina, Suína, Frango, etc).
        /// </summary>
        public CarneEnum.TipoCarne TipoCarne { get; set; }

        /// <summary>
        /// Unidade de medida (ex: KG).
        /// </summary>
        public string UnidadeMedida { get; set; } = string.Empty;

    }
}
