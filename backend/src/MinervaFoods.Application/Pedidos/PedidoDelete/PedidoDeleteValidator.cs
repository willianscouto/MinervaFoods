using FluentValidation;

namespace MinervaFoods.Application.Pedidos.PedidoDelete
{
    /// <summary>
    /// Validador para <see cref="PedidoDeleteCommand"/> que define as regras de validação para exclusão de um pedido.
    /// </summary>
    public class PedidoDeleteValidator : AbstractValidator<PedidoDeleteCommand>
    {
        /// <summary>
        /// Inicializa as regras de validação para <see cref="PedidoDeleteCommand"/>.
        /// </summary>
        public PedidoDeleteValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O ID do pedido é obrigatório.");
        }
    }
}
