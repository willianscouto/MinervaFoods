using AutoMapper;

namespace MinervaFoods.Api.Features.Carnes.CarneGet
{
    /// <summary>
    /// Profile for mapping CarneGet feature carne to commands
    /// </summary>
    public class CarneGetProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CarneGet feature
        /// </summary>
        public CarneGetProfile()
        {
            CreateMap<Guid, Application.Carnes.CarneGet.CarneGetCommand>()
                .ConstructUsing(id => new Application.Carnes.CarneGet.CarneGetCommand(id));

        
        }
    }
}
