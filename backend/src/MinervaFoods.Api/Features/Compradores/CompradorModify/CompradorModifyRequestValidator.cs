using FluentValidation;
using MinervaFoods.Domain.Enums;
using MinervaFoods.Domain.Validation.Common;

namespace MinervaFoods.Api.Features.Compradores.CompradorModify
{
    /// <summary>
    /// Validador para o comando <see cref="CompradorModifyCommand"/>, responsável por aplicar regras de validação
    /// na atualização de dados de um comprador.
    /// </summary>
    /// <remarks>
    /// Regras de validação aplicadas:
    /// <list type="bullet">
    /// <item><description><strong>Id</strong>: obrigatório e deve ser um GUID válido (diferente de vazio).</description></item>
    /// <item><description><strong>Nome</strong>: obrigatório e com no máximo 200 caracteres.</description></item>
    /// <item><description><strong>Documento</strong>: obrigatório e com no máximo 20 caracteres.</description></item>
    /// <item><description><strong>Email</strong>: validado por um validador específico de e-mail.</description></item>
    /// <item><description><strong>Campos opcionais</strong>: possuem limites máximos de caracteres e são validados apenas quando preenchidos.</description></item>
    /// <item><description><strong>DataNascimento</strong>: se informada, deve ser anterior à data atual.</description></item>
    /// </list>
    /// </remarks>
    public class CompradorModifyRequestValidator : AbstractValidator<CompradorModifyRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompradorModifyRequestValidator"/> class 
        /// with defined validation rules for project creation.
        /// </summary>
        public CompradorModifyRequestValidator()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O ID do comprador deve ser um GUID válido.");

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
                .When(c => !string.IsNullOrWhiteSpace(c.Telefone));

            RuleFor(c => c.Logradouro)
                .MaximumLength(200).WithMessage("O logradouro deve ter no máximo 200 caracteres.")
                .When(c => !string.IsNullOrWhiteSpace(c.Logradouro));

            RuleFor(c => c.Complemento)
                .MaximumLength(100).WithMessage("O complemento deve ter no máximo 100 caracteres.")
                .When(c => !string.IsNullOrWhiteSpace(c.Complemento));

            RuleFor(c => c.Bairro)
                .MaximumLength(100).WithMessage("O bairro deve ter no máximo 100 caracteres.")
                .When(c => !string.IsNullOrWhiteSpace(c.Bairro));

            RuleFor(c => c.Cidade)
                .MaximumLength(100).WithMessage("A cidade deve ter no máximo 100 caracteres.")
                .When(c => !string.IsNullOrWhiteSpace(c.Cidade));

            RuleFor(c => c.Estado)
                .MaximumLength(2).WithMessage("O estado deve ter no máximo 2 caracteres.")
                .When(c => !string.IsNullOrWhiteSpace(c.Estado));

            RuleFor(c => c.Cep)
                .MaximumLength(10).WithMessage("O CEP deve ter no máximo 10 caracteres.")
                .When(c => !string.IsNullOrWhiteSpace(c.Cep));

            RuleFor(c => c.Pais)
                .MaximumLength(100).WithMessage("O país deve ter no máximo 100 caracteres.")
                .When(c => !string.IsNullOrWhiteSpace(c.Pais));

            RuleFor(c => c.DataNascimento)
                .LessThan(DateTime.Today).WithMessage("A data de nascimento deve ser anterior à data atual.")
                .When(c => c.DataNascimento.HasValue);

        }
    }
}
