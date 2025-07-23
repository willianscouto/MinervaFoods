using FluentValidation;
using MinervaFoods.Domain.Validation.Common;

namespace MinervaFoods.Api.Features.Compradores.CompradorCreate
{
    /// <summary>
    /// Validador para o comando <see cref="CompradorCreateRequest"/> que define regras para criação de um comprador.
    /// </summary>
    /// <remarks>
    /// Regras de validação incluem:
    /// <list type="bullet">
    /// <item><description><strong>Nome</strong>: Obrigatório. Máximo de 200 caracteres.</description></item>
    /// <item><description><strong>Documento</strong>: Obrigatório. Máximo de 20 caracteres.</description></item>
    /// <item><description><strong>Email</strong>: Validação através de <see cref="EmailValidator"/>.</description></item>
    /// <item><description><strong>Telefone</strong>: Opcional. Máximo de 20 caracteres.</description></item>
    /// <item><description><strong>Logradouro</strong>: Opcional. Máximo de 200 caracteres.</description></item>
    /// <item><description><strong>Complemento</strong>: Opcional. Máximo de 100 caracteres.</description></item>
    /// <item><description><strong>Bairro</strong>: Opcional. Máximo de 100 caracteres.</description></item>
    /// <item><description><strong>Cidade</strong>: Opcional. Máximo de 100 caracteres.</description></item>
    /// <item><description><strong>Estado</strong>: Opcional. Máximo de 2 caracteres.</description></item>
    /// <item><description><strong>CEP</strong>: Opcional. Máximo de 10 caracteres.</description></item>
    /// <item><description><strong>País</strong>: Obrigatório. Máximo de 100 caracteres.</description></item>
    /// <item><description><strong>Data de Nascimento</strong>: Opcional. Deve ser anterior à data atual.</description></item>
    /// </list>
    /// </remarks>
    public class CompradorCreateRequestValidator : AbstractValidator<CompradorCreateRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompradorCreateRequestValidator"/> class 
        /// with defined validation rules for project creation.
        /// </summary>
        public CompradorCreateRequestValidator()
        {
            RuleFor(c => c.Nome)
                 .NotEmpty().WithMessage("O nome é obrigatório.")
                 .MaximumLength(200).WithMessage("O nome deve ter no máximo 200 caracteres.");

            RuleFor(c => c.Documento)
                .NotEmpty().WithMessage("O documento é obrigatório.")
                .MaximumLength(20).WithMessage("O documento deve ter no máximo 20 caracteres.");

            RuleFor(c => c.Email)
                .SetValidator(new EmailValidator());

            RuleFor(c => c.Telefone)
                .MaximumLength(20).WithMessage("O telefone deve ter no máximo 20 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Telefone));

            RuleFor(c => c.Logradouro)
                .MaximumLength(200).WithMessage("O logradouro deve ter no máximo 200 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Logradouro));

            RuleFor(c => c.Complemento)
                .MaximumLength(100).WithMessage("O complemento deve ter no máximo 100 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Complemento));

            RuleFor(c => c.Bairro)
                .MaximumLength(100).WithMessage("O bairro deve ter no máximo 100 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Bairro));

            RuleFor(c => c.Cidade)
                .MaximumLength(100).WithMessage("A cidade deve ter no máximo 100 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Cidade));

            RuleFor(c => c.Estado)
                .MaximumLength(2).WithMessage("O estado deve ter no máximo 2 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Estado));

            RuleFor(c => c.Cep)
                .MaximumLength(10).WithMessage("O CEP deve ter no máximo 10 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Cep));

            RuleFor(c => c.Pais)
                .MaximumLength(100).WithMessage("O país deve ter no máximo 100 caracteres.")
                  .When(c => !string.IsNullOrEmpty(c.Cep));

            RuleFor(c => c.DataNascimento)
                .LessThan(DateTime.Today).WithMessage("A data de nascimento deve ser anterior à data atual.")
                .When(c => c.DataNascimento.HasValue);
        }
    }
}
