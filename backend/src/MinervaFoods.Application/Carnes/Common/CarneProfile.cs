using AutoMapper;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.Carnes.Common
{
    /// <summary>
    /// Profile de mapeamento entre a entidade Carne e o modelo CarneResult.
    /// </summary>
    public class CarneProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos para a entidade Carne.
        /// </summary>
        public CarneProfile()
        {
            CreateMap<Carne, CarneResult>();
        }
    }
}
