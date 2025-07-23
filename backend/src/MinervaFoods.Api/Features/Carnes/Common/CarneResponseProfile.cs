using AutoMapper;
using MinervaFoods.Api.Features.Carnes.Common;
using MinervaFoods.Application.Carnes.Common;

namespace MinervaFoods.Api.Features.Carnes.Common
{
    /// <summary>
    /// Profile for mapping between Carne entity and CarneResponse
    /// </summary>
    public class CarneResponseProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CarneResponse operation
        /// </summary>
        public CarneResponseProfile()
        {
            CreateMap<CarneResponse, CarneResult>().ReverseMap();
        }
    }
}
