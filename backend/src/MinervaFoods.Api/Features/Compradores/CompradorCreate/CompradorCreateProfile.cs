using AutoMapper;
using MinervaFoods.Api.Features.Compradores.Common;
using MinervaFoods.Application.Compradores.CompradorCreate;

namespace MinervaFoods.Api.Features.Compradores.CompradorCreate
{
    /// <summary>
    /// Profile for mapping between Application and API CreateProject responses
    /// </summary>
    public class CompradorCreateProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateProject feature
        /// </summary>
        public CompradorCreateProfile()
        {
            CreateMap<CompradorCreateRequest, CompradorCreateCommand>();
            CreateMap<CompradorCreateResult, CompradorResponse>();
        }
    }
}
