using AutoMapper;
using MinervaFoods.Api.Features.Pedidos.Common;
using MinervaFoods.Application.Pedidos.PedidoCreate;

namespace MinervaFoods.Api.Features.Pedidos.PedidoCreate
{
    /// <summary>
    /// Profile for mapping between Application and API CreateProject responses
    /// </summary>
    public class PedidoCreateProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateProject feature
        /// </summary>
        public PedidoCreateProfile()
        {
            CreateMap<PedidoCreateRequest, PedidoCreateCommand>();
            CreateMap<PedidoCreateResult, PedidoResponse>();
        }
    }
}
