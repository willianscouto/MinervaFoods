using FluentValidation;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Domain.Validators
{
    /// <summary>
    /// Validador para a entidade <see cref="PedidoItem"/>.
    /// Garante que os dados do item do pedido estejam corretos antes de persistência.
    /// </summary>
    public class PedidoItemValidator : AbstractValidator<PedidoItem>
    {
        public PedidoItemValidator()
        {
            RuleFor(i => i.PedidoId)
                .NotEmpty()
                .WithMessage("O identificador do pedido é obrigatório.");

            RuleFor(i => i.CarneId)
                .NotEmpty()
                .WithMessage("O identificador da carne é obrigatório.");

            RuleFor(i => i.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade deve ser maior que zero.");

            RuleFor(i => i.PrecoUnitario)
                .GreaterThan(0)
                .WithMessage("O preço unitário deve ser maior que zero.");

            RuleFor(i => i.Cotacao)
                .GreaterThan(0)
                .WithMessage("A cotação deve ser maior que zero.");

            RuleFor(c => c.Moeda)
                  .IsInEnum().WithMessage("Tipo de moeda inválido.")
                  .NotEqual(PedidoItemEnum.Moeda.Unknown).WithMessage("Tipo de moeda deve ser especificado.");

        }
    }
}
