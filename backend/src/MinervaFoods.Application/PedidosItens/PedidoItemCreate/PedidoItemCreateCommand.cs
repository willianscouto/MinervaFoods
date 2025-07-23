using MediatR;
using MinervaFoods.Application.PedidosItens.Common;
using MinervaFoods.Domain.Enums;
using System.Collections.Generic;

namespace MinervaFoods.Application.PedidosItens.PedidoItemCreate
{
    /// <summary>
    /// Comando para criar itens de pedido.
    /// </summary>
    public class PedidoItemCreateCommand : IRequest<ICollection<PedidoItemResult>>
    {
        /// <summary>
        /// Lista de itens de pedido a serem criados.
        /// </summary>
        public List<PedidoItemCreateItem> Itens { get; set; } = new();
    }

    /// <summary>
    /// Representa um item individual do pedido.
    /// </summary>
    public class PedidoItemCreateItem
    {
      

        /// <summary>
        /// Identificador da carne associada ao item.
        /// </summary>
        public Guid CarneId { get; private set; }

        /// <summary>
        /// Quantidade da carne no pedido (em KG).
        /// </summary>
        public decimal Quantidade { get; private set; }

        /// <summary>
        /// Moeda utilizada para o preço do item.
        /// </summary>
        public PedidoItemEnum.Moeda Moeda { get; private set; }

        /// <summary>
        /// Preço unitário da carne na moeda especificada.
        /// </summary>
        public decimal PrecoUnitario { get; private set; }

        /// <summary>
        ///  total do item (quantidade * preço unitário) na moeda do item.
        /// </summary>
        public decimal Total { get; private set; }

        /// <summary>
        ///  total do item (quantidade * preço unitário) na moeda do item.
        /// </summary>
        public decimal Cotacao { get; private set; }

        /// <summary>
        /// Valor total do item Total X Cotacao na moeda do item.
        /// </summary>
        public decimal ValorTotalCotacao { get; private set; }
    }
}
