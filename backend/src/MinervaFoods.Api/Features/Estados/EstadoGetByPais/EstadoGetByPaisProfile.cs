using AutoMapper;

namespace MinervaFoods.Api.Features.Estados.EstadoGetByPais
{
    public class EstadoGetByPaisProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for ProjectGetByUserAndName feature
        /// </summary>
        public EstadoGetByPaisProfile()
        {
            CreateMap<Guid, MinervaFoods.Application.Estados.EstadoGetByPais.EstadoGetByPaisCommand>()
                .ConstructUsing(id => new MinervaFoods.Application.Estados.EstadoGetByPais.EstadoGetByPaisCommand(id));


        }
    }
}
