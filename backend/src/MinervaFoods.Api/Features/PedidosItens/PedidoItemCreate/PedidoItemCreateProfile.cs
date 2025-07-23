using AutoMapper;
using MinervaFoods.Api.Features.PedidosItens.Common;
using MinervaFoods.Application.PedidosItens.Common;
using MinervaFoods.Application.PedidosItens.PedidoItemCreate;

namespace MinervaFoods.Api.Features.PedidosItens.PedidoItemCreate
{
    /// <summary>
    /// Profile for mapping between Application and API CreateProject responses
    /// </summary>
    public class PedidoItemCreateProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateProject feature
        /// </summary>
        public PedidoItemCreateProfile()
        {
            CreateMap<PedidoItemCreateRequest, PedidoItemCreateItem>();
            CreateMap<PedidoItemResult, PedidoItemResponse>();
        }
    }
}
