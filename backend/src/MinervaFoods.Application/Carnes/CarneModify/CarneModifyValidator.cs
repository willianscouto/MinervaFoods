using FluentValidation;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Application.Carnes.CarneModify
{
    /// <summary>
    /// Validador para o comando <see cref="CarneModifyCommand"/>, responsável por aplicar regras de validação
    /// na atualização de dados de uma carne.
    /// </summary>
    /// <remarks>
    /// Regras de validação aplicadas:
    /// <list type="bullet">
    /// <item><description><strong>Id</strong>: obrigatório e deve ser um GUID válido (diferente de vazio).</description></item>
    /// <item><description><strong>Ean</strong>: obrigatório e deve conter exatamente 13 dígitos numéricos.</description></item>
    /// <item><description><strong>Nome</strong>: obrigatório e deve ter no máximo 100 caracteres.</description></item>
    /// <item><description><strong>TipoCarne</strong>: obrigatório e deve representar um valor válido do enum <see cref="TipoCarne"/>.</description></item>
    /// <item><description><strong>UnidadeMedida</strong>: obrigatória.</description></item>
    /// </list>
    /// </remarks>
    public class CarneModifyValidator : AbstractValidator<CarneModifyCommand>
    {
        /// <summary>
        /// Inicializa uma nova instância de <see cref="CarneModifyValidator"/>, definindo as regras de validação.
        /// </summary>
        public CarneModifyValidator()
        {
            RuleFor(c => c.Id)
               .NotEqual(Guid.Empty).WithMessage("O ID da carne deve ser um GUID válido.");

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
