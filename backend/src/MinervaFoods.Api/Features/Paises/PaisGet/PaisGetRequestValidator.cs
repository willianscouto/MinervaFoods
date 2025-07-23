using FluentValidation;

namespace MinervaFoods.Api.Features.Paises.PaisGet
{

    /// <summary>
    /// Validator for PaisGetRequest
    /// </summary>
    public class PaisGetRequestValidator : AbstractValidator<PaisGetRequest>
    {
        /// <summary>
        /// Initializes validation rules for PaisGetRequest
        /// </summary>
        public PaisGetRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("País ID is required");
        }
    }
}
