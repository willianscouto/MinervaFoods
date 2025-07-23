using FluentValidation;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Domain.Validators
{
    /// <summary>
    /// Validador para a entidade <see cref="Carne"/>.
    /// Garante que os dados da carne estejam corretos antes da persistência.
    /// </summary>
    public class CarneValidator : AbstractValidator<Carne>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CarneValidator"/>.
        /// </summary>
        public CarneValidator()
        {
            RuleFor(c => c.Ean)
                .NotEmpty().WithMessage("O código EAN é obrigatório.")
                .Matches(@"^\d{13}$").WithMessage("O código EAN deve conter exatamente 13 dígitos numéricos.");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome da carne é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da carne deve ter no máximo 100 caracteres.");

            RuleFor(c => c.TipoCarne)
                .IsInEnum().WithMessage("Tipo de carne inválido.")
                .NotEqual(CarneEnum.TipoCarne.Unknown).WithMessage("Tipo de carne deve ser especificado.");

            RuleFor(c => c.UnidadeMedida)
                .NotEmpty().WithMessage("A unidade de medida é obrigatória.");
        }
    }
}
