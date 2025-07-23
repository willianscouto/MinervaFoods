using FluentValidation;

namespace MinervaFoods.Application.Estados.EstadoGet
{
    /// <summary>
    /// Validador para <see cref="CarneGetCommand"/>.
    /// </summary>
    public class EstadoGetValidator : AbstractValidator<EstadoGetCommand>
    {
        /// <summary>
        /// Inicializa as regras de validação para <see cref="CarneGetCommand"/>.
        /// </summary>
        public EstadoGetValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O ID da carne deve ser um GUID válido.");
        }
    }
}
