using FluentValidation;
using MediatR;
using MinervaFoods.Application.Compradores.Common;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.Compradores.CompradorModify
{
    /// <summary>
    /// Comando para modificar os dados de um comprador.
    /// </summary>
    /// <remarks>
    /// Este comando é utilizado para capturar os dados necessários para atualização de um comprador.
    /// Ele implementa <see cref="IRequest{TResponse}"/> para iniciar a solicitação que retorna um 
    /// <see cref="CompradorResult"/>.
    ///
    /// Os dados fornecidos neste comando são validados utilizando a classe 
    /// <see cref="CompradorModifyValidator"/>, que estende <see cref="AbstractValidator{T}"/>,
    /// garantindo que os campos estejam preenchidos corretamente e sigam as regras de negócio.
    /// </remarks>
    public class CompradorModifyCommand : IRequest<CompradorResult>
    {
        /// <summary>
        /// Identificador único do comprador a ser atualizado.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome completo do comprador.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Documento do comprador (CPF, CNPJ, etc.).
        /// </summary>
        public string Documento { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do comprador.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Número de telefone do comprador (opcional).
        /// </summary>
        public string? Telefone { get; set; }

        /// <summary>
        /// Endereço (logradouro) do comprador (opcional).
        /// </summary>
        public string? Logradouro { get; set; }

        /// <summary>
        /// Complemento do endereço (opcional).
        /// </summary>
        public string? Complemento { get; set; }

        /// <summary>
        /// Bairro do endereço do comprador (opcional).
        /// </summary>
        public string? Bairro { get; set; }

        /// <summary>
        /// Cidade do endereço do comprador (opcional).
        /// </summary>
        public string? Cidade { get; set; }

        /// <summary>
        /// Estado (UF) do endereço do comprador (opcional).
        /// </summary>
        public string? Estado { get; set; }

        /// <summary>
        /// Código postal (CEP) do comprador (opcional).
        /// </summary>
        public string? Cep { get; set; }

        /// <summary>
        /// País do comprador (opcional).
        /// </summary>
        public string? Pais { get; set; }

        /// <summary>
        /// Data de nascimento do comprador (opcional).
        /// </summary>
        public DateTime? DataNascimento { get; set; }

       

        /// <summary>
        /// Valida a instância atual do comando e retorna os resultados da validação.
        /// </summary>
        /// <returns>
        /// Um objeto <see cref="ValidationResultDetail"/> contendo o resultado da validação.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new CompradorModifyValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(error => (ValidationErrorDetail)error)
            };
        }
    }
}
