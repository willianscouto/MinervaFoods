using FluentValidation;

namespace MinervaFoods.Api.Features.Carnes.CarneGet
{

    /// <summary>
    /// Validator for CarneGetRequest
    /// </summary>
    public class CarneGetRequestValidator : AbstractValidator<CarneGetRequest>
    {
        /// <summary>
        /// Initializes validation rules for CarneGetRequest
        /// </summary>
        public CarneGetRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Carne ID is required");
        }
    }
}
