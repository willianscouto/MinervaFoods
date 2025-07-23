namespace MinervaFoods.Application.Compradores.Common
{
    /// <summary>
    /// Representa o resultado da operação com a entidade Comprador.
    /// </summary>
    public class CompradorResult
    {
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
        public string Pais { get; set; } = string.Empty;

        /// <summary>
        /// Data de nascimento do comprador (opcional).
        /// </summary>
        public DateTime? DataNascimento { get; set; }
    }
}
