using FluentValidation;
using MediatR;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.Compradores.CompradorCreate
{
    /// <summary>
    /// Comando para criação de um novo comprador.
    /// </summary>
    /// <remarks>
    /// Este comando é utilizado para capturar os dados necessários para a criação de um comprador.
    /// Ele implementa <see cref="IRequest{TResponse}"/> para iniciar a requisição que retorna
    /// um <see cref="CompradorCreateResult"/>.
    ///
    /// Os dados fornecidos neste comando são validados utilizando o 
    /// <see cref="CompradorCreateValidator"/>, que estende 
    /// <see cref="AbstractValidator{T}"/> para garantir que os campos estejam devidamente
    /// preenchidos e sigam as regras estabelecidas.
    /// </remarks>
    public class CompradorCreateCommand : IRequest<CompradorCreateResult>
    {
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
        /// País do comprador.
        /// </summary>
        public string? Pais { get; set; }

        /// <summary>
        /// Data de nascimento do comprador (opcional).
        /// </summary>
        public DateTime? DataNascimento { get; set; }

        /// <summary>
        /// Valida a instância atual do comando e retorna os resultados da validação.
        /// </summary>
        /// <remarks>
        /// Este método utiliza um validador interno para realizar a validação dos dados.
        /// </remarks>
        /// <returns>
        /// Um objeto <see cref="ValidationResultDetail"/> contendo o resultado da validação.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new CompradorCreateValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
