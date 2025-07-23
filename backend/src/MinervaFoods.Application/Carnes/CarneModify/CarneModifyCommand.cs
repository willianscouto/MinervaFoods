using FluentValidation;
using MediatR;
using MinervaFoods.Application.Carnes.Common;
using MinervaFoods.Domain.Enums;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.Carnes.CarneModify
{
    /// <summary>
    /// Comando para modificar os dados de uma carne.
    /// </summary>
    /// <remarks>
    /// Este comando é utilizado para capturar os dados necessários para atualização de uma carne.
    /// Ele implementa <see cref="IRequest{TResponse}"/> para iniciar a solicitação que retorna um 
    /// <see cref="CarneResult"/>.
    ///
    /// Os dados fornecidos neste comando são validados utilizando a classe 
    /// <see cref="CarneModifyValidator"/>, que estende <see cref="AbstractValidator{T}"/>,
    /// garantindo que os campos estejam preenchidos corretamente e sigam as regras de negócio.
    /// </remarks>
    public class CarneModifyCommand : IRequest<CarneResult>
    {
        /// <summary>
        /// Código identificador da carne.
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
        /// Tipo da carne (ex: Bovina, Suína, Frango, etc).
        /// </summary>
        public CarneEnum.TipoCarne TipoCarne { get; set; }

        /// <summary>
        /// Unidade de medida (ex: KG).
        /// </summary>
        public string UnidadeMedida { get; set; } = string.Empty;

        /// <summary>
        /// Realiza a validação da instância atual e retorna os resultados.
        /// </summary>
        /// <remarks>
        /// Este método utiliza o validador interno <see cref="CarneModifyValidator"/> 
        /// para aplicar as regras de validação.
        /// </remarks>
        /// <returns>
        /// Um objeto <see cref="ValidationResultDetail"/> contendo o resultado da validação.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new CarneModifyValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
