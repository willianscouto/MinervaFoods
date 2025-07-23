using MediatR;
using MinervaFoods.Domain.Common;
using MinervaFoods.Domain.Enums;
using MinervaFoods.Domain.Validators;
using MinervaFoods.Helpers;
using System.Xml.Linq;

namespace MinervaFoods.Domain.Entities
{
    /// <summary>
    /// Representa uma carne que pode ser adicionada a um pedido.
    /// </summary>
    public class Carne : BaseEntity
    {
        /// <summary>
        /// Código internacional do produto (EAN).
        /// </summary>
        public string Ean { get; private set; } = string.Empty;

        /// <summary>
        /// Nome da carne (ex: Picanha, Costela Suína).
        /// </summary>
        public string Nome { get; private set; } = string.Empty;

        /// <summary>
        /// Tipo da carne (Bovina, Suína, Frango, etc).
        /// </summary>
        public CarneEnum.TipoCarne TipoCarne { get; private set; }

        /// <summary>
        /// Unidade de medida (sempre KG por padrão).
        /// </summary>
        public string UnidadeMedida { get; private set; } = "KG";

        /// <summary>
        /// Construtor protegido para uso do Entity Framework.
        /// </summary>
        public Carne() { }

        /// <summary>
        /// Construtor da entidade Carne.
        /// </summary>
        /// <param name="ean">Código EAN.</param>
        /// <param name="nome">Nome da carne.</param>
        /// <param name="tipoCarne">Tipo da carne.</param>
        /// <param name="unidadeMedida">Unidade de medida (padrão: KG).</param>
        public Carne(string ean, string nome, CarneEnum.TipoCarne tipoCarne, string? unidadeMedida = "KG")
        {
            Ean = ean;
            Nome = nome;
            TipoCarne = tipoCarne;
            UnidadeMedida = string.IsNullOrWhiteSpace(unidadeMedida) ? "KG" : unidadeMedida;
        }


        /// <summary>
        /// Updates the Carne details.
        /// </summary>
        public void Update(string ean, string nome, CarneEnum.TipoCarne tipoCarne, string? unidadeMedida = "KG")
        {
            Ean = ean;
            Nome = nome;
            TipoCarne = tipoCarne;
            UnidadeMedida = string.IsNullOrWhiteSpace(unidadeMedida) ? "KG" : unidadeMedida;

        }
        /// <summary>
        /// Valida os dados da carne usando regras de negócio.
        /// </summary>
        /// <returns>Resultado da validação contendo erros, se houver.</returns>
        public ValidationResultDetail Validar()
        {
            var validator = new CarneValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
            };
        }
    }
}
