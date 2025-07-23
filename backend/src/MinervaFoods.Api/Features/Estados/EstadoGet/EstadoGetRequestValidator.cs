using FluentValidation;

namespace MinervaFoods.Api.Features.Estados.EstadoGet
{

    /// <summary>
    /// Validator for EstadoGetRequest
    /// </summary>
    public class EstadoGetRequestValidator : AbstractValidator<EstadoGetRequest>
    {
        /// <summary>
        /// Initializes validation rules for EstadoGetRequest
        /// </summary>
        public EstadoGetRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Estado ID is required");
        }
    }
}
