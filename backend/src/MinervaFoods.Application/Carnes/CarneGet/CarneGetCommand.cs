using MediatR;
using MinervaFoods.Application.Carnes.Common;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.Carnes.CarneGet
{
    /// <summary>
    /// Comando para obter uma carne pelo seu identificador.
    /// </summary>
    public class CarneGetCommand : IRequest<CarneResult>
    {
        /// <summary>
        /// Obtém o identificador único da carne a ser consultada.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="CarneGetCommand"/>.
        /// </summary>
        /// <param name="id">Identificador único da carne.</param>
        public CarneGetCommand(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Valida a instância atual do comando e retorna os resultados da validação.
        /// </summary>
        /// <remarks>
        /// Este método utiliza um validador interno para executar a validação baseada nas regras definidas para este comando.
        /// Certifique-se de que a instância esteja corretamente preenchida antes de chamar este método.
        /// </remarks>
        /// <returns>Um objeto <see cref="ValidationResultDetail"/> contendo o resultado da validação.</returns>
        public ValidationResultDetail Validate()
        {
            var validator = new CarneGetValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(error => (ValidationErrorDetail)error)
            };
        }
    }
}
