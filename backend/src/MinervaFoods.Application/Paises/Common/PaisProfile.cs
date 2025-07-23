using AutoMapper;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.Paises.Common
{
    /// <summary>
    /// Profile de mapeamento entre a entidade Pais e o modelo PaisResult.
    /// </summary>
    public class PaisProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos para a entidade Carne.
        /// </summary>
        public PaisProfile()
        {
            CreateMap<Pais, PaisResult>();
        }
    }
}
