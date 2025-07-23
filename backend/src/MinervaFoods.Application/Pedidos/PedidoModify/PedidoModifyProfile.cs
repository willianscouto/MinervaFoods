using AutoMapper;
using MinervaFoods.Application.Compradores.Common;
using MinervaFoods.Application.Compradores.CompradorModify;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.Pedidos.PedidoModify
{
    /// <summary>
    /// Profile para mapeamento entre entidades de Pedido e seus DTOs relacionados à modificação de pedido.
    /// </summary>
    public class CompradorModifyProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos utilizados na operação de modificação de pedido.
        /// </summary>
        public CompradorModifyProfile()
        {
            CreateMap<CompradorModifyCommand, Comprador>().ReverseMap();
            CreateMap<Comprador, CompradorResult>();
        }
    }
}
