using AutoMapper;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.PedidosItens.Common
{
    /// <summary>
    /// Profile de mapeamento entre a entidade PedidoItem e o modelo PedidoItemResult.
    /// </summary>
    public class PedidoItemProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos para a entidade PedidoItem.
        /// </summary>
        public PedidoItemProfile()
        {
            CreateMap<PedidoItem, PedidoItemResult>();
        }
    }
}
