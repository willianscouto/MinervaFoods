using AutoMapper;

namespace MinervaFoods.Api.Features.Estados.EstadoGet
{
    /// <summary>
    /// Profile for mapping EstadoGet feature requests to commands
    /// </summary>
    public class EstadoGetProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for EstadoGet feature
        /// </summary>
        public EstadoGetProfile()
        {
            CreateMap<Guid, MinervaFoods.Application.Estados.EstadoGet.EstadoGetCommand>()
                .ConstructUsing(id => new MinervaFoods.Application.Estados.EstadoGet.EstadoGetCommand(id));

        
        }
    }
}
