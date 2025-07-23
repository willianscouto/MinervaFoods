using FluentValidation;
using MinervaFoods.Api.Features.PedidosItens.PedidoItemCreate;
using MinervaFoods.Application.PedidosItens.PedidoItemCreate;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Api.Features.Pedidos.PedidoCreate
{
    /// <summary>
    /// Validator for <see cref="CarneCreateRequest"/> that defines validation rules for carne creation.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// <list type="bullet">
    /// <item><description><strong>TaskId</strong>: Required. Must be a valid (non-empty) GUID.</description></item>
    /// <item><description><strong>Text</strong>: Required. Maximum 500 characters.</description></item>
    /// </list>
    /// </remarks>
    public class PedidoCreateRequestValidator : AbstractValidator<PedidoCreateRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarneCreateRequestValidator"/> class 
        /// with defined validation rules for project creation.
        /// </summary>
        public PedidoCreateRequestValidator()
        {
            RuleFor(p => p.CompradorId)
                .NotEmpty()
                .WithMessage("O identificador do comprador é obrigatório.");

            RuleFor(p => p.DataPedido)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("A data do pedido não pode ser no futuro.");

            RuleForEach(p => p.PedidoItem)
                .SetValidator(new PedidoItemCreateRequestValidator()); 
        }
    }
}
