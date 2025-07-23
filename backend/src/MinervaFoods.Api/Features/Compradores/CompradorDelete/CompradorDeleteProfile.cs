using AutoMapper;

namespace MinervaFoods.Api.Features.Compradores.CompradorDelete
{
    /// <summary>
    /// Profile for mapping Comprador feature requests to commands
    /// </summary>
    public class CompradorDeleteProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CompradorDeleteCommand feature
        /// </summary>
        public CompradorDeleteProfile()
        {
            CreateMap<Guid, Application.Compradores.CompradorDelete.CompradorDeleteCommand>()
                .ConstructUsing(id => new Application.Compradores.CompradorDelete.CompradorDeleteCommand(id));
        }
    }
}
