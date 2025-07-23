using FluentValidation;

namespace MinervaFoods.Application.Pedidos.PedidoGet
{
    /// <summary>
    /// Validador para <see cref="PedidoGetCommand"/>.
    /// </summary>
    public class PedidoGetValidator : AbstractValidator<PedidoGetCommand>
    {
        /// <summary>
        /// Inicializa as regras de validação para <see cref="PedidoGetCommand"/>.
        /// </summary>
        public PedidoGetValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O ID do pedido deve ser um GUID válido.");
        }
    }
}
