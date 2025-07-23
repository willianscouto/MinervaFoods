using AutoMapper;
using MinervaFoods.Application.PedidosItens.Common;
using MinervaFoods.Application.PedidosItens.PedidoItemCreate;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.PedidosItens.PedidoItemModify
{
    public class PedidoItemModifyProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos para a operação de criação de comprador.
        /// </summary>
        public PedidoItemModifyProfile()
        {
            CreateMap<PedidoItemModifyItem, PedidoItem>().ReverseMap();
            CreateMap<PedidoItemModifyItem, PedidoItemCreateItem>().ReverseMap(); 
            CreateMap<PedidoItem, PedidoItemResult>();
        }
    }
}
