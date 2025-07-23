using MediatR;
using MinervaFoods.Application.Estados.Common;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.Estados.EstadoGet
{
    /// <summary>
    /// Comando para obter um estado pelo seu identificador.
    /// </summary>
    public class EstadoGetCommand : IRequest<EstadoResult>
    {
        /// <summary>
        /// Obtém o identificador único do estado a ser consultado.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="EstadoGetCommand"/>.
        /// </summary>
        /// <param name="id">Identificador único do estado.</param>
        public EstadoGetCommand(Guid id)
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
            var validator = new EstadoGetValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(error => (ValidationErrorDetail)error)
            };
        }
    }
}
