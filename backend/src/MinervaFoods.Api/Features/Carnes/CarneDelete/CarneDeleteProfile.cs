using AutoMapper;

namespace MinervaFoods.Api.Features.Carnes.CarneDelete
{
    /// <summary>
    /// Profile for mapping CarneDelete feature requests to commands
    /// </summary>
    public class CarneDeleteProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CarneDeleteCommand feature
        /// </summary>
        public CarneDeleteProfile()
        {
            CreateMap<Guid, Application.Carnes.CarneDelete.CarneDeleteCommand>()
                .ConstructUsing(id => new Application.Carnes.CarneDelete.CarneDeleteCommand(id));
        }
    }
}
