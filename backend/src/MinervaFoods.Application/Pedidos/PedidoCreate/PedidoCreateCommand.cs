using FluentValidation;
using MediatR;
using MinervaFoods.Application.PedidosItens.PedidoItemCreate;
using MinervaFoods.Domain.Enums;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.Pedidos.PedidoCreate
{
    /// <summary>
    /// Comando para criação de um novo pedido.
    /// </summary>
    /// <remarks>
    /// Este comando é utilizado para capturar os dados necessários para a criação de um pedido.
    /// Ele implementa <see cref="IRequest{TResponse}"/> para iniciar a requisição que retorna
    /// um <see cref="PedidoCreateResult"/>.
    ///
    /// Os dados fornecidos neste comando são validados utilizando o 
    /// <see cref="PedidoCreateValidator"/>, que estende 
    /// <see cref="AbstractValidator{T}"/> para garantir que os campos estejam devidamente
    /// preenchidos e sigam as regras estabelecidas.
    /// </remarks>
    public class PedidoCreateCommand : IRequest<PedidoCreateResult>
    {
        /// <summary>
        /// Identificador do comprador responsável pelo pedido.
        /// </summary>
        public Guid CompradorId { get; set; }

   

        /// <summary>
        /// Data de criação do pedido.
        /// </summary>
        public DateTime DataPedido { get; set; }


        /// <summary>
        /// Observacao do pedido.
        /// </summary>
        public string? Observacao { get; set; }


        /// <summary>
        /// Lista de itens que compõem o pedido.
        /// </summary>
        public ICollection<PedidoItemCreateItem> PedidoItem { get; set; } = new List<PedidoItemCreateItem>();

        /// <summary>
        /// Valida a instância atual do comando e retorna os resultados da validação.
        /// </summary>
        /// <remarks>
        /// Este método utiliza um validador específico para garantir que os dados estejam corretos.
        /// </remarks>
        /// <returns>
        /// Um objeto <see cref="ValidationResultDetail"/> contendo o resultado da validação.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new PedidoCreateValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
