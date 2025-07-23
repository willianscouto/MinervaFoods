using FluentValidation;

namespace MinervaFoods.Api.Features.Carnes.CarneDelete
{
    /// <summary>
    /// Request model for deleting a carne
    /// </summary>
    public class CarneDeleteRequestValidator : AbstractValidator<CarneDeleteRequest>
    {
        /// <summary>
        /// Initializes validation rules for CarneDeleteRequest
        /// </summary>
        public CarneDeleteRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Carne ID is required");
        }
    }
}
