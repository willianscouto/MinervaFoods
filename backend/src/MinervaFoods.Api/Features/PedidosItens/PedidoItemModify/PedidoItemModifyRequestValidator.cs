using FluentValidation;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Api.Features.PedidosItens.PedidoItemModify
{
    /// <summary>
    /// Validator for PedidoItemModifyRequest that defines validation rules for user creation.
    /// </summary>
    public class PedidoItemModifyRequestValidator : AbstractValidator<PedidoItemModifyRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PedidoItemModifyRequestValidator"/> class 
        /// with defined validation rules for project creation.
        /// </summary>
        public PedidoItemModifyRequestValidator()
        {
            RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("O ID do comprador deve ser um GUID válido.");

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
