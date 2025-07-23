using MediatR;
using MinervaFoods.Application.Estados.Common;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.Estados.EstadoGetByPais
{
    /// <summary>
    /// Comando para obter os estados associados a um país específico.
    /// </summary>
    public class EstadoGetByPaisCommand : IRequest<IEnumerable<EstadoResult>>
    {
        /// <summary>
        /// Identificador único do país cujos estados serão consultados.
        /// </summary>
        public Guid PaisId { get; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="EstadoGetByPaisCommand"/>.
        /// </summary>
        /// <param name="paisId">Identificador único do país.</param>
        public EstadoGetByPaisCommand(Guid paisId)
        {
            PaisId = paisId;
        }

        /// <summary>
        /// Valida a instância atual do comando e retorna os resultados da validação.
        /// </summary>
        /// <remarks>
        /// Este método utiliza um validador específico para verificar se os dados do comando estão corretos
        /// antes de processar a solicitação.
        /// </remarks>
        /// <returns>Um objeto <see cref="ValidationResultDetail"/> contendo o resultado da validação.</returns>
        public ValidationResultDetail Validate()
        {
            var validator = new EstadoGetByPaisValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(error => (ValidationErrorDetail)error)
            };
        }
    }
}
