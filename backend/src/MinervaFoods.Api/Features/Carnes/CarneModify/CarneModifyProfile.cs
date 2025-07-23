using AutoMapper;
using MinervaFoods.Application.Carnes.CarneModify;

namespace MinervaFoods.Api.Features.Carnes.CarneModify
{
    /// <summary>
    /// Profile for mapping between Application and API CarneModify responses
    /// </summary>
    public class CarneModifyProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CarneModify feature
        /// </summary>
        public CarneModifyProfile()
        {
            CreateMap<CarneModifyRequest, CarneModifyCommand>();
         
        }
    }
}
