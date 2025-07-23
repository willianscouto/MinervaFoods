using FluentValidation;

namespace MinervaFoods.Application.Paises.PaisGet
{
    /// <summary>
    /// Validador para <see cref="PaisGetCommand"/>.
    /// </summary>
    public class PaisGetValidator : AbstractValidator<PaisGetCommand>
    {
        /// <summary>
        /// Inicializa as regras de validação para <see cref="PaisGetCommand"/>.
        /// </summary>
        public PaisGetValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O ID do país deve ser um GUID válido.");
        }
    }
}
