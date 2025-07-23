using MediatR;
using MinervaFoods.Application.Compradores.Common;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.Compradores.CompradorGet
{
    /// <summary>
    /// Comando para obter um comprador pelo seu identificador.
    /// </summary>
    public class CompradorGetCommand : IRequest<CompradorResult>
    {
        /// <summary>
        /// Obtém o identificador único do comprador a ser consultado.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="CompradorGetCommand"/>.
        /// </summary>
        /// <param name="id">Identificador único do comprador.</param>
        public CompradorGetCommand(Guid id)
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
            var validator = new CompradorGetValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(error => (ValidationErrorDetail)error)
            };
        }
    }
}
