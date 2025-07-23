using AutoMapper;
using MinervaFoods.Application.Pedidos.Common;

namespace MinervaFoods.Api.Features.Pedidos.Common
{
    /// <summary>
    /// Profile for mapping between Carne entity and PedidoResponse
    /// </summary>
    public class PedidoResponseProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for PedidoResponse operation
        /// </summary>
        public PedidoResponseProfile()
        {
            CreateMap<PedidoResult, PedidoResponse>().ReverseMap();
        }
    }
}
