using FluentValidation;
using MediatR;
using MinervaFoods.Application.Compradores.Common;
using MinervaFoods.Application.Pedidos.Common;
using MinervaFoods.Application.PedidosItens.PedidoItemModify;
using MinervaFoods.Domain.Enums;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.Pedidos.PedidoModify
{
    /// <summary>
    /// Comando para modificar os dados de um pedido.
    /// </summary>
    /// <remarks>
    /// Este comando é utilizado para capturar os dados necessários para atualização de um pedido.
    /// Ele implementa <see cref="IRequest{TResponse}"/> para iniciar a solicitação que retorna um 
    /// <see cref="PedidoResult"/>.
    ///
    /// Os dados fornecidos neste comando são validados utilizando a classe 
    /// <see cref="PedidoModifyValidator"/>, que estende <see cref="AbstractValidator{T}"/>,
    /// garantindo que os campos estejam preenchidos corretamente e sigam as regras de negócio.
    /// </remarks>
    public class PedidoModifyCommand : IRequest<PedidoResult>
    {
        /// <summary>
        /// Identificador único do pedido a ser atualizado.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador do comprador responsável pelo pedido.
        /// </summary>
        public Guid CompradorId { get; set; }

        /// <summary>
        /// Número do pedido.
        /// </summary>
        public long NumeroPedido { get; set; }

        /// <summary>
        /// Data de criação do pedido.
        /// </summary>
        public DateTime DataPedido { get; set; }

        /// <summary>
        /// Status atual do pedido (por exemplo: Aberto, Finalizado, Cancelado).
        /// </summary>
        public PedidoEnum.Status StatusPedido { get; set; }


        /// <summary>
        /// Observacao do pedido.
        /// </summary>
        public string Observacao { get; set; } = string.Empty;

        /// <summary>
        /// Lista de itens que compõem o pedido.
        /// </summary>
        public ICollection<PedidoItemModifyItem> PedidoItem { get; set; } = new List<PedidoItemModifyItem>();

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
            var validator = new PedidoModifyValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
