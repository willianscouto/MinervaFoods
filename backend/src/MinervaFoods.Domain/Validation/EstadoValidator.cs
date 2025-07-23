using FluentValidation;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Domain.Validators
{
    /// <summary>
    /// Validador para a entidade <see cref="Estado"/>.
    /// </summary>
    public class EstadoValidator : AbstractValidator<Estado>
    {
        public EstadoValidator()
        {
            RuleFor(e => e.CodPais)
                .NotEmpty().WithMessage("O código do país é obrigatório.");

            RuleFor(e => e.Sigla)
                .NotEmpty().WithMessage("A sigla do estado é obrigatória.")
                .Length(2).WithMessage("A sigla do estado deve conter exatamente 2 caracteres.");

            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("O nome do estado é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do estado deve ter no máximo 100 caracteres.");
        }
    }
}
