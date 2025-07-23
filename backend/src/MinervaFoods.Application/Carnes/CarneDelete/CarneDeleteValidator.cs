using FluentValidation;

namespace MinervaFoods.Application.Carnes.CarneDelete
{
    /// <summary>
    /// Validador para <see cref="CarneDeleteCommand"/> que define as regras de validação para exclusão de carne.
    /// </summary>
    public class CarneDeleteValidator : AbstractValidator<CarneDeleteCommand>
    {
        /// <summary>
        /// Inicializa as regras de validação para <see cref="CarneDeleteCommand"/>.
        /// </summary>
        public CarneDeleteValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O ID da carne é obrigatório.");
        }
    }
}
