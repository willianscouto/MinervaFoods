using AutoMapper;
using MinervaFoods.Application.Pedidos.PedidoModify;

namespace MinervaFoods.Api.Features.Pedidos.PedidoModify
{
    /// <summary>
    /// Profile for mapping between Application and API PedidoModify responses
    /// </summary>
    public class PedidoModifyProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for PedidoModify feature
        /// </summary>
        public PedidoModifyProfile()
        {
            CreateMap<PedidoModifyRequest, PedidoModifyCommand>().ReverseMap();
         
        }
    }
}
