using AutoMapper;
using MinervaFoods.Application.PedidosItens.Common;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.PedidosItens.PedidoItemCreate
{
    public class PedidoItemCreateProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos para a operação de criação de comprador.
        /// </summary>
        public PedidoItemCreateProfile()
        {
            CreateMap<PedidoItemCreateItem, PedidoItem>().ReverseMap();
            CreateMap<PedidoItem, PedidoItemResult>();
        }
    }
}
