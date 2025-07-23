using AutoMapper;
using MinervaFoods.Application.Estados.Common;

namespace MinervaFoods.Api.Features.Estados.Common
{
    /// <summary>
    /// Profile for mapping between Estado entity and EstadoResponse
    /// </summary>
    public class EstadoResponseProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for EstadoResponse operation
        /// </summary>
        public EstadoResponseProfile()
        {
            CreateMap<EstadoResponse, EstadoResult>().ReverseMap();
        }
    }
}
