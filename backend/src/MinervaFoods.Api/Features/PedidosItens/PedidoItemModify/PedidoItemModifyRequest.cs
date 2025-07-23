using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Api.Features.PedidosItens.PedidoItemModify
{
    /// <summary>
    /// Represents a request to modify an existing pedido in the system.
    /// </summary>
    public class PedidoItemModifyRequest
    {
        /// <summary>
        /// The unique identifier of the pedido
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador da carne associada ao item.
        /// </summary>
        public Guid CarneId { get;  set; }

        /// <summary>
        /// Quantidade da carne no pedido (em KG).
        /// </summary>
        public decimal Quantidade { get;  set; }

        /// <summary>
        /// Moeda utilizada para o preço do item.
        /// </summary>
        public PedidoItemEnum.Moeda Moeda { get;  set; }

        /// <summary>
        /// Preço unitário da carne na moeda especificada.
        /// </summary>
        public decimal PrecoUnitario { get;  set; }

        /// <summary>
        ///  total do item (quantidade * preço unitário) na moeda do item.
        /// </summary>
        public decimal Total { get;  set; }

        /// <summary>
        ///  total do item (quantidade * preço unitário) na moeda do item.
        /// </summary>
        public decimal Cotacao { get;  set; }

        /// <summary>
        /// Valor total do item Total X Cotacao na moeda do item.
        /// </summary>
        public decimal ValorTotalCotacao { get;  set; }

    }
}
