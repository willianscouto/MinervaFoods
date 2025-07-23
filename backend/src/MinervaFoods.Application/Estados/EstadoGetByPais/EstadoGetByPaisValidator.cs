using FluentValidation;

namespace MinervaFoods.Application.Estados.EstadoGetByPais
{
    /// <summary>
    /// Validador para <see cref="EstadoGetByPaisCommand"/>.
    /// </summary>
    public class EstadoGetByPaisValidator : AbstractValidator<EstadoGetByPaisCommand>
    {
        /// <summary>
        /// Inicializa as regras de validação para <see cref="EstadoGetByPaisCommand"/>.
        /// </summary>
        public EstadoGetByPaisValidator()
        {
            RuleFor(command => command.PaisId)
                .NotEqual(Guid.Empty)
                .WithMessage("O ID do país deve ser um GUID válido.");
        }
    }
}
