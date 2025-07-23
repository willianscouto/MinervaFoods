using FluentValidation;
using MinervaFoods.Api.Features.PedidosItens.PedidoItemModify;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Api.Features.Pedidos.PedidoModify
{
    /// <summary>
    /// Validator for PedidoModifyRequest that defines validation rules for user creation.
    /// </summary>
    public class PedidoModifyRequestValidator : AbstractValidator<PedidoModifyRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PedidoModifyRequestValidator"/> class 
        /// with defined validation rules for project creation.
        /// </summary>
        public PedidoModifyRequestValidator()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O ID do pedido deve ser um GUID válido.");

            RuleFor(p => p.CompradorId)
                .NotEmpty()
                .WithMessage("O identificador do comprador é obrigatório.");

            RuleFor(p => p.DataPedido)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("A data do pedido não pode ser no futuro.");

            RuleFor(p => p.StatusPedido)
                .NotEqual(PedidoEnum.Status.Unknown)
                .WithMessage("Status do pedido inválido.");

            RuleForEach(p => p.PedidoItem)
                .SetValidator(new PedidoItemModifyRequestValidator());
        }
    }
}
