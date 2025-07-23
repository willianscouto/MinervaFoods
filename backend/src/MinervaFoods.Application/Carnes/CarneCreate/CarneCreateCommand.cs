using FluentValidation;
using MediatR;
using MinervaFoods.Domain.Enums;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.Carnes.CarneCreate
{
    /// <summary>
    /// Comando para criação de uma nova carne.
    /// </summary>
    /// <remarks>
    /// Este comando é utilizado para capturar os dados necessários para a criação de uma carne.
    /// Ele implementa <see cref="IRequest{TResponse}"/> para iniciar a requisição que retorna
    /// um <see cref="CarneCreateResult"/>.
    ///
    /// Os dados fornecidos neste comando são validados utilizando o 
    /// <see cref="CarneCreateValidator"/>, que estende 
    /// <see cref="AbstractValidator{T}"/> para garantir que os campos estejam devidamente
    /// preenchidos e sigam as regras estabelecidas.
    /// </remarks>
    public class CarneCreateCommand : IRequest<CarneCreateResult>
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

        /// <summary>
        /// Valida a instância atual do comando e retorna os resultados da validação.
        /// </summary>
        /// <remarks>
        /// Este método utiliza um validador interno para realizar a validação dos dados.
        /// </remarks>
        /// <returns>
        /// Um objeto <see cref="ValidationResultDetail"/> contendo o resultado da validação.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new CarneCreateValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
