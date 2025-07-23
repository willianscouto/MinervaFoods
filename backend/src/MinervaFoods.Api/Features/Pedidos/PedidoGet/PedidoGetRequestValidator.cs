using FluentValidation;

namespace MinervaFoods.Api.Features.Pedidos.PedidoGet
{

    /// <summary>
    /// Validator for PedidoGetRequest
    /// </summary>
    public class PedidoGetRequestValidator : AbstractValidator<PedidoGetRequest>
    {
        /// <summary>
        /// Initializes validation rules for PedidoGetRequest
        /// </summary>
        public PedidoGetRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Pedido ID is required");
        }
    }
}
