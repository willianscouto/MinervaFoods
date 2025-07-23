using AutoMapper;
using MinervaFoods.Application.Estados.Common;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.Estados.Common
{
    /// <summary>
    /// Profile de mapeamento entre a entidade Estado e o modelo EstadoResult.
    /// </summary>
    public class EstadoProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos para a entidade Estado.
        /// </summary>
        public EstadoProfile()
        {
            CreateMap<Estado, EstadoResult>();
        }
    }
}
