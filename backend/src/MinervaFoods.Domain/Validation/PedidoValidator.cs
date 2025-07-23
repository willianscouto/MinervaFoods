using FluentValidation;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Enums;
using MinervaFoods.Domain.Validation;

namespace MinervaFoods.Domain.Validators
{
    /// <summary>
    /// Validador para a entidade <see cref="Pedido"/>.
    /// Garante que os dados do pedido estejam corretos antes de persistência.
    /// </summary>
    public class PedidoValidator : AbstractValidator<Pedido>
    {
        public PedidoValidator()
        {
            RuleFor(p => p.CompradorId)
                .NotEmpty()
                .WithMessage("O identificador do comprador é obrigatório.");

            RuleFor(p => p.DataPedido)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("A data do pedido não pode ser no futuro.");

            RuleFor(c => c.StatusPedido)
             .NotEqual(PedidoEnum.Status.Unknown)
                .WithMessage("Status do pedido inválido.");

            RuleFor(p => p.PedidoItem)
                .NotEmpty()
                .WithMessage("O pedido deve conter pelo menos um item.");

            RuleForEach(p => p.PedidoItem)
                .SetValidator(new PedidoItemValidator());
        }
    }
}
