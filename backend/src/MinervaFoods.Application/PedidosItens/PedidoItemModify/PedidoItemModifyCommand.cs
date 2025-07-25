﻿using MediatR;
using MinervaFoods.Application.PedidosItens.Common;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Application.PedidosItens.PedidoItemModify
{
    /// <summary>
    /// Comando para criar itens de pedido.
    /// </summary>
    public class PedidoItemModifyCommand : IRequest<ICollection<PedidoItemResult>>
    {
        /// <summary>
        /// Lista de itens de pedido a serem criados.
        /// </summary>
        public List<PedidoItemModifyItem> Itens { get; set; } = new();
    }

    /// <summary>
    /// Representa um item individual do pedido.
    /// </summary>
    public class PedidoItemModifyItem
    {
         ///<summary>
        /// Identificador único do comprador a ser atualizado.
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
