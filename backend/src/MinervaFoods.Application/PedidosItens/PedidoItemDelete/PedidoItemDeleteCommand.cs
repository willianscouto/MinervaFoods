using MediatR;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.PedidosItens.PedidoItemDelete
{
    /// <summary>
    /// Command para deletar um pedido.
    /// </summary>
    public class PedidoItemDeleteCommand : IRequest<PedidoItemDeleteResult>
    {
        /// <summary>
        /// Identificador único do pedido a ser deletado.
        /// </summary>
        public IEnumerable<Guid> Ids { get; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="PedidoDeleteCommand"/>.
        /// </summary>
        /// <param name="id">ID do pedido a ser deletado.</param>
        public PedidoItemDeleteCommand(IEnumerable<Guid> ids)
        {
            Ids = ids;
        }

        /// <summary>
        /// Valida a instância atual do comando de deleção de pedido e retorna o resultado da validação.
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
            var validator = new PedidoItemDeleteValidator(); 
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(error => (ValidationErrorDetail)error)
            };
        }
    }
}
