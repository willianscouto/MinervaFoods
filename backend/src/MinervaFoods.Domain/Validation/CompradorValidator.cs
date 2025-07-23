using FluentValidation;
using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Validation.Common;

namespace MinervaFoods.Domain.Validation
{
    /// <summary>
    /// Validador para a entidade Comprador, garantindo que os dados essenciais estejam corretos.
    /// </summary>
    public class CompradorValidator : AbstractValidator<Comprador>
    {
        public CompradorValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(200).WithMessage("O nome deve ter no máximo 200 caracteres.");

            RuleFor(c => c.Documento)
                .NotEmpty().WithMessage("O documento é obrigatório.")
                .MaximumLength(20).WithMessage("O documento deve ter no máximo 20 caracteres.");

            RuleFor(user => user.Email).SetValidator(new EmailValidator());

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
                .NotEmpty().WithMessage("O país é obrigatório.")
                .MaximumLength(100).WithMessage("O país deve ter no máximo 100 caracteres.");

            RuleFor(c => c.DataNascimento)
                .LessThan(DateTime.Today).WithMessage("A data de nascimento deve ser anterior à data atual.")
                .When(c => c.DataNascimento.HasValue);
        }
    }
}



