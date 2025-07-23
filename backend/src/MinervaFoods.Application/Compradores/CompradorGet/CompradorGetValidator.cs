using FluentValidation;

namespace MinervaFoods.Application.Compradores.CompradorGet
{
    /// <summary>
    /// Validador para <see cref="CompradorGetCommand"/>.
    /// </summary>
    public class CompradorGetValidator : AbstractValidator<CompradorGetCommand>
    {
        /// <summary>
        /// Inicializa as regras de validação para <see cref="CompradorGetCommand"/>.
        /// </summary>
        public CompradorGetValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O ID do comprador deve ser um GUID válido.");
        }
    }
}
