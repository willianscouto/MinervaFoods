using FluentValidation;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Domain.Validators
{
    /// <summary>
    /// Validador para a entidade <see cref="Pais"/>.
    /// </summary>
    public class PaisValidator : AbstractValidator<Pais>
    {
        public PaisValidator()
        {
            RuleFor(p => p.Sigla)
                .NotEmpty().WithMessage("A sigla do país é obrigatória.")
                .Length(2).WithMessage("A sigla do país deve conter exatamente 2 caracteres.");

            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage("O nome do país é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do país deve ter no máximo 100 caracteres.");
        }
    }
}
