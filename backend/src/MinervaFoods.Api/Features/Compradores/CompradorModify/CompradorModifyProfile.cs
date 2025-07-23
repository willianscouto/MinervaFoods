using AutoMapper;
using MinervaFoods.Application.Compradores.CompradorModify;

namespace MinervaFoods.Api.Features.Compradores.CompradorModify
{
    /// <summary>
    /// Profile for mapping between Application and API CarneModify responses
    /// </summary>
    public class CompradorModifyProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CarneModify feature
        /// </summary>
        public CompradorModifyProfile()
        {
            CreateMap<CompradorModifyRequest, CompradorModifyCommand>();
         
        }
    }
}
