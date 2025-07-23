using AutoMapper;

namespace MinervaFoods.Api.Features.Pedidos.PedidoDelete
{
    /// <summary>
    /// Profile for mapping PedidoDelete feature requests to commands
    /// </summary>
    public class PedidoDeleteProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for PedidoDeleteCommand feature
        /// </summary>
        public PedidoDeleteProfile()
        {
            CreateMap<Guid, Application.Pedidos.PedidoDelete.PedidoDeleteCommand>()
                .ConstructUsing(id => new Application.Pedidos.PedidoDelete.PedidoDeleteCommand(id));
        }
    }
}
