using FluentValidation;

namespace MinervaFoods.Application.PedidosItens.PedidoItemDelete
{
    /// <summary>
    /// Validador para <see cref="PedidoDeleteCommand"/> que define as regras de validação para exclusão de um pedido.
    /// </summary>
    public class PedidoItemDeleteValidator : AbstractValidator<PedidoItemDeleteCommand>
    {
        /// <summary>
        /// Inicializa as regras de validação para <see cref="PedidoDeleteCommand"/>.
        /// </summary>
        public PedidoItemDeleteValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty()
                .WithMessage("O ID do pedido é obrigatório.");
        }
    }
}
