using AutoMapper;

namespace MinervaFoods.Api.Features.Pedidos.PedidoGet
{
    /// <summary>
    /// Profile for mapping PedidoGet feature carne to commands
    /// </summary>
    public class PedidoGetProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CarneGet feature
        /// </summary>
        public PedidoGetProfile()
        {
            CreateMap<Guid, Application.Pedidos.PedidoGet.PedidoGetCommand>()
                .ConstructUsing(id => new Application.Pedidos.PedidoGet.PedidoGetCommand(id));

        
        }
    }
}
