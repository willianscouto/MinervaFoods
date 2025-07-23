using AutoMapper;
using MinervaFoods.Application.Pedidos.Common;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.Pedidos.PedidoModify
{
    /// <summary>
    /// Profile para mapeamento entre entidades de Pedido e seus DTOs relacionados à modificação de pedido.
    /// </summary>
    public class PedidoModifyProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos utilizados na operação de modificação de pedido.
        /// </summary>
        public PedidoModifyProfile()
        {
            CreateMap<PedidoModifyCommand, Pedido>().ReverseMap();
            CreateMap<Pedido, PedidoResult>();
        }
    }
}
