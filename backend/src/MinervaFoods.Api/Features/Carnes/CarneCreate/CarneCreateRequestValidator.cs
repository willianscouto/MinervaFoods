using FluentValidation;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Api.Features.Carnes.CarneCreate
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
    public class CarneCreateRequestValidator : AbstractValidator<CarneCreateRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarneCreateRequestValidator"/> class 
        /// with defined validation rules for project creation.
        /// </summary>
        public CarneCreateRequestValidator()
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
