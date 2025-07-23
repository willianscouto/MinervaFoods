using AutoMapper;
using MinervaFoods.Application.Carnes.Common;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.Carnes.CarneModify
{
    /// <summary>
    /// Profile para mapeamento entre entidades de Carne e seus DTOs relacionados à modificação de carnes.
    /// </summary>
    public class CarneModifyProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos utilizados na operação de modificação de carnes.
        /// </summary>
        public CarneModifyProfile()
        {
            CreateMap<CarneModifyCommand, Carne>().ReverseMap();
            CreateMap<Carne, CarneResult>();
        }
    }
}
