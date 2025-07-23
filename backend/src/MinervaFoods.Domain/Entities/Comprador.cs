using MinervaFoods.Domain.Common;
using MinervaFoods.Domain.Validation;
using MinervaFoods.Helpers;

namespace MinervaFoods.Domain.Entities
{

    /// <summary>
    /// Representa um comprador que realiza pedidos de carnes.
    /// </summary>
    public class Comprador: BaseEntity
    {
       
        /// <summary>
        /// Nome completo do comprador.
        /// </summary>
        public string Nome { get; private set; } = string.Empty;

        /// <summary>
        /// Documento do comprador (CPF, CNPJ, etc.).
        /// </summary>
        public string Documento { get; private set; } = string.Empty;

        /// <summary>
        /// E-mail do comprador.
        /// </summary>
        public string Email { get; private set; } = string.Empty;

        /// <summary>
        /// Número de telefone do comprador (opcional).
        /// </summary>
        public string? Telefone { get; private set; }

        /// <summary>
        /// Endereço (logradouro) do comprador (opcional).
        /// </summary>
        public string? Logradouro { get; private set; }

        /// <summary>
        /// Complemento do endereço (opcional).
        /// </summary>
        public string? Complemento { get; private set; }

        /// <summary>
        /// Bairro do endereço do comprador (opcional).
        /// </summary>
        public string? Bairro { get; private set; }

        /// <summary>
        /// Cidade do endereço do comprador (opcional).
        /// </summary>
        public string? Cidade { get; private set; }

        /// <summary>
        /// Estado (UF) do endereço do comprador (opcional).
        /// </summary>
        public string? Estado { get; private set; }

        /// <summary>
        /// Código postal (CEP) do comprador (opcional).
        /// </summary>
        public string? Cep { get; private set; }

        /// <summary>
        /// País do comprador.
        /// </summary>
        public string? Pais { get; private set; }

        /// <summary>
        /// Data de nascimento do comprador (opcional).
        /// </summary>
        public DateTime? DataNascimento { get; private set; }

        public Comprador() { }

        public Comprador(
            string nome,
            string documento,
            string email,
            string? telefone,
            string? logradouro,
            string? complemento,
            string? bairro,
            string? cidade,
            string? estado,
            string? cep,
            string pais,
            DateTime? dataNascimento)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Documento = documento;
            Email = email;
            Telefone = telefone;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;
            Pais = pais;
            DataNascimento = dataNascimento;
        }


        /// <summary>
        /// Atualiza os dados do comprador.
        /// </summary>
        public void Update(
                string documento,
                string nome,
                string email,
                string? telefone,
                string? logradouro,
                string? complemento,
                string? bairro,
                string? estado,
                string? cep,
                string? pais,
                DateTime? dataNascimento)
        {
            Documento = documento;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Estado = estado;
            Cep = cep;
            Pais = pais;
            DataNascimento = dataNascimento;
        }

        /// <summary>
        /// Valida os dados do comprador usando regras de negócio.
        /// </summary>
        /// <returns>
        /// Um <see cref="ValidationResultDetail"/> com o resultado da validação.
        /// </returns>
        public ValidationResultDetail Validar()
        {
            var validator = new CompradorValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
            };
        }
    }
}

