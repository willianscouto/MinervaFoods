using FluentValidation;

namespace MinervaFoods.Application.Estados.EstadoGet
{
    /// <summary>
    /// Validador para <see cref="EstadoGetCommand"/>.
    /// </summary>
    public class EstadoGetValidator : AbstractValidator<EstadoGetCommand>
    {
        /// <summary>
        /// Inicializa as regras de validação para <see cref="EstadoGetCommand"/>.
        /// </summary>
        public EstadoGetValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O ID do estado deve ser um GUID válido.");
        }
    }
}
