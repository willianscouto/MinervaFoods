using AutoMapper;
using MinervaFoods.Api.Features.Carnes.Common;
using MinervaFoods.Application.Carnes.CarneCreate;

namespace MinervaFoods.Api.Features.Carnes.CarneCreate
{
    /// <summary>
    /// Profile for mapping between Application and API CreateProject responses
    /// </summary>
    public class CarneCreateProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateProject feature
        /// </summary>
        public CarneCreateProfile()
        {
            CreateMap<CarneCreateRequest, CarneCreateCommand>();
            CreateMap<CarneCreateResult, CarneResponse>();
        }
    }
}
