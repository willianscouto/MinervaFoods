using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Application.PedidosItens.Common
{
    /// <summary>
    /// Representa o resultado da operação com a entidade Pedido Item.
    /// </summary>
    public class PedidoItemResult
    {
        /// <summary>
        /// Identificador do pedido ao qual o item pertence.
        /// </summary>
        public Guid PedidoId { get; set; }

        /// <summary>
        /// Identificador da carne associada ao item.
        /// </summary>
        public Guid CarneId { get; set; }

        public Carne Carne { get; set; } = null!;

        /// <summary>
        /// Quantidade da carne no pedido (em KG).
        /// </summary>
        public decimal Quantidade { get; set; }

        /// <summary>
        /// Moeda utilizada para o preço do item.
        /// </summary>
        public PedidoItemEnum.Moeda Moeda { get; set; }

        /// <summary>
        /// Preço unitário da carne na moeda especificada.
        /// </summary>
        public decimal PrecoUnitario { get; set; }

        /// <summary>
        ///  total do item (quantidade * preço unitário) na moeda do item.
        /// </summary>
        public decimal Total { get;  }

        /// <summary>
        ///  total do item (quantidade * preço unitário) na moeda do item.
        /// </summary>
        public decimal Cotacao { get; set; }

        /// <summary>
        /// Valor total do item Total X Cotacao na moeda do item.
        /// </summary>
        public decimal ValorTotalCotacao { get;  }
    }
}
