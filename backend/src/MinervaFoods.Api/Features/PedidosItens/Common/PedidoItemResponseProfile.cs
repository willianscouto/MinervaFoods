using AutoMapper;
using MinervaFoods.Application.PedidosItens.Common;

namespace MinervaFoods.Api.Features.PedidosItens.Common
{
    /// <summary>
    /// Profile for mapping between PedidoItem entity and PedidoItemResponse
    /// </summary>
    public class PedidoItemResponseProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for PedidoItemResponse operation
        /// </summary>
        public PedidoItemResponseProfile()
        {
            CreateMap<PedidoItemResponse, PedidoItemResult>().ReverseMap();
        }
    }
}
