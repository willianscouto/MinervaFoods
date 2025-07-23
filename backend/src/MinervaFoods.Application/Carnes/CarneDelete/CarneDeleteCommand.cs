using MediatR;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.Carnes.CarneDelete
{
    /// <summary>
    /// Command para deletar uma carne.
    /// </summary>
    public class CarneDeleteCommand : IRequest<CarneDeleteResult>
    {
        /// <summary>
        /// Identificador único da carne a ser deletada.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="CarneDeleteCommand"/>.
        /// </summary>
        /// <param name="id">ID da carne a ser deletada.</param>
        public CarneDeleteCommand(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Valida a instância atual do comando de deleção de carne e retorna o resultado da validação.
        /// </summary>
        /// <remarks>
        /// Este método utiliza um validador interno para realizar a validação.
        /// Certifique-se de que o ID esteja corretamente preenchido antes de chamar este método.
        /// </remarks>
        /// <returns>
        /// Um objeto <see cref="ValidationResultDetail"/> contendo o resultado da validação.
        /// A propriedade <see cref="ValidationResultDetail.IsValid"/> indica se a validação foi bem-sucedida,
        /// e <see cref="ValidationResultDetail.Errors"/> contém os erros de validação, se houver.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new CarneDeleteValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(error => (ValidationErrorDetail)error)
            };
        }
    }
}
