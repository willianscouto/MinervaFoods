using FluentValidation;

namespace MinervaFoods.Api.Features.Compradores.CompradorGet
{

    /// <summary>
    /// Validator for CompradorGetRequest
    /// </summary>
    public class CompradorGetRequestValidator : AbstractValidator<CompradorGetRequest>
    {
        /// <summary>
        /// Initializes validation rules for CompradorGetRequest
        /// </summary>
        public CompradorGetRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Comprador ID is required");
        }
    }
}
