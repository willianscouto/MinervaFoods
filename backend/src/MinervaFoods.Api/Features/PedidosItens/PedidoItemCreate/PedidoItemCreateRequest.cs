using MinervaFoods.Application.PedidosItens.PedidoItemCreate;
using MinervaFoods.Domain.Enums;

namespace MinervaFoods.Api.Features.PedidosItens.PedidoItemCreate
{
    /// <summary>
    /// Represents a request to create a new carne in the system.
    /// </summary>
    public class PedidoItemCreateRequest
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
