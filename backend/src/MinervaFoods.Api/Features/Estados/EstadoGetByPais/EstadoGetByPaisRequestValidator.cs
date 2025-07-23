using FluentValidation;

namespace MinervaFoods.Api.Features.Estados.EstadoGetByPais
{
    /// <summary>
    /// Validator for EstadoGetByPaisRequest
    /// </summary>
    public class EstadoGetByPaisRequestValidator : AbstractValidator<EstadoGetByPaisRequest>
    {
        /// <summary>
        /// Initializes validation rules for EstadoGetByPaisRequest
        /// </summary>
        public EstadoGetByPaisRequestValidator()
        {
            RuleFor(project => project.PaisId)
                 .NotEqual(Guid.Empty).WithMessage("PaisId must be a valid GUID.");

          


        }
    }
}
