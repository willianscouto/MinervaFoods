using AutoMapper;

namespace MinervaFoods.Api.Features.Compradores.CompradorGet
{
    /// <summary>
    /// Profile for mapping CompradorGet feature carne to commands
    /// </summary>
    public class CompradorGetProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CompradorGet feature
        /// </summary>
        public CompradorGetProfile()
        {
            CreateMap<Guid, Application.Carnes.CarneGet.CarneGetCommand>()
                .ConstructUsing(id => new Application.Carnes.CarneGet.CarneGetCommand(id));

        
        }
    }
}
