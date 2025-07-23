using AutoMapper;
using MinervaFoods.Application.Paises.Common;

namespace MinervaFoods.Api.Features.Paises.Common
{
    /// <summary>
    /// Profile for mapping between Project entity and ProjectResponse
    /// </summary>
    public class PaisResponseProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for ProjectResponse operation
        /// </summary>
        public PaisResponseProfile()
        {
            CreateMap<PaisResponse, PaisResult>().ReverseMap();
        }
    }
}
