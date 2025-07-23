using AutoMapper;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.Pedidos.PedidoCreate
{
    /// <summary>
    /// Profile do AutoMapper para mapeamento entre entidades de Pedido e os DTOs relacionados à criação de pedido.
    /// </summary>
    public class PedidoCreateProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos para a operação de criação de pedido.
        /// </summary>
        public PedidoCreateProfile()
        {
            CreateMap<PedidoCreateCommand, Pedido>().ReverseMap();
            CreateMap<Pedido, PedidoCreateResult>();
        }
    }
}
