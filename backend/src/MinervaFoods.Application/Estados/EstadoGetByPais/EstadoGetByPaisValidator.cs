using FluentValidation;

namespace MinervaFoods.Application.Estados.CarneGet
{
    /// <summary>
    /// Validador para <see cref="CarneGetCommand"/>.
    /// </summary>
    public class CarneGetValidator : AbstractValidator<CarneGetCommand>
    {
        /// <summary>
        /// Inicializa as regras de validação para <see cref="CarneGetCommand"/>.
        /// </summary>
        public CarneGetValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O ID da carne deve ser um GUID válido.");
        }
    }
}
