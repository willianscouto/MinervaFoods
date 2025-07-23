using AutoMapper;
using MinervaFoods.Application.Compradores.Common;

namespace MinervaFoods.Api.Features.Compradores.Common
{
    /// <summary>
    /// Profile for mapping between Comprador entity and CompradorResponse
    /// </summary>
    public class CompradorResponseProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CompradorResponse operation
        /// </summary>
        public CompradorResponseProfile()
        {
            CreateMap<CompradorResponse, CompradorResult>().ReverseMap();
        }
    }
}
