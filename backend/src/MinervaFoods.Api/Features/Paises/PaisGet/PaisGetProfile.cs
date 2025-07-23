using AutoMapper;

namespace MinervaFoods.Api.Features.Products.PaisGet
{
    /// <summary>
    /// Profile for mapping PaisesGet feature requests to commands
    /// </summary>
    public class ProjectGetProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for PaisGet feature
        /// </summary>
        public ProjectGetProfile()
        {
            CreateMap<Guid, Application.Paises.PaisGet.PaisGetCommand>()
                .ConstructUsing(id => new Application.Paises.PaisGet.PaisGetCommand(id));

        
        }
    }
}
