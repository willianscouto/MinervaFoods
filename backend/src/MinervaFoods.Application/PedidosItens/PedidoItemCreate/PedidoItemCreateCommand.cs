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
        public Guid PedidoId { get; set; }
    }

    /// <summary>
    /// Representa um item individual do pedido.
    /// </summary>
    public class PedidoItemCreateItem
    {
      

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

       
    }
}
