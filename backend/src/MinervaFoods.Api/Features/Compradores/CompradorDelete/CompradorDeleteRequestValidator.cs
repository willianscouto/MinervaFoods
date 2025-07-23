using FluentValidation;

namespace MinervaFoods.Api.Features.Compradores.CompradorDelete
{
    /// <summary>
    /// Request model for deleting a comprador
    /// </summary>
    public class CompradorDeleteRequestValidator : AbstractValidator<CompradorDeleteRequest>
    {
        /// <summary>
        /// Initializes validation rules for CompradorDeleteRequest
        /// </summary>
        public CompradorDeleteRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Comprador ID is required");
        }
    }
}
