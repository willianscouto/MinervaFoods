using AutoMapper;
using MinervaFoods.Application.PedidosItens.PedidoItemModify;

namespace MinervaFoods.Api.Features.PedidosItens.PedidoItemModify
{
    /// <summary>
    /// Profile for mapping between Application and API PedidoItemModify requests/responses
    /// </summary>
    public class PedidoItemModifyProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for PedidoItemModify feature
        /// </summary>
        public PedidoItemModifyProfile()
        {
           
            CreateMap<PedidoItemModifyRequest, PedidoItemModifyItem>();

          
            CreateMap<IEnumerable<PedidoItemModifyRequest>, PedidoItemModifyCommand>()
                .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src));
        }
    }
}
