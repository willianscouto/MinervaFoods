using FluentValidation;

namespace MinervaFoods.Application.Compradores.CompradorDelete
{
    /// <summary>
    /// Validador para <see cref="CompradorDeleteCommand"/> que define as regras de validação para exclusão de um comprador.
    /// </summary>
    public class CompradorDeleteValidator : AbstractValidator<CompradorDeleteCommand>
    {
        /// <summary>
        /// Inicializa as regras de validação para <see cref="CompradorDeleteCommand"/>.
        /// </summary>
        public CompradorDeleteValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O ID do comprador é obrigatório.");
        }
    }
}
