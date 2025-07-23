using FluentValidation;

namespace MinervaFoods.Api.Features.Pedidos.PedidoDelete
{
    /// <summary>
    /// Request model for deleting a pedido
    /// </summary>
    public class PedidoDeleteRequestValidator : AbstractValidator<PedidoDeleteRequest>
    {
        /// <summary>
        /// Initializes validation rules for PedidoDeleteRequest
        /// </summary>
        public PedidoDeleteRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Pedido ID is required");
        }
    }
}
