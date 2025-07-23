using AutoMapper;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.Pedidos.Common
{
    /// <summary>
    /// Profile de mapeamento entre a entidade Pedido e o modelo PedidoResult.
    /// </summary>
    public class PedidoProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos para a entidade Pedido.
        /// </summary>
        public PedidoProfile()
        {
            CreateMap<Pedido, PedidoResult>();
        }
    }
}
