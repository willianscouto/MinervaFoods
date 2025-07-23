using AutoMapper;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.Carnes.CarneCreate
{
    /// <summary>
    /// Profile do AutoMapper para mapeamento entre entidades de Carne e os DTOs relacionados à criação de carne.
    /// </summary>
    public class CarneCreateProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos para a operação de criação de carne.
        /// </summary>
        public CarneCreateProfile()
        {
            
            CreateMap<CarneCreateCommand, Carne>().ReverseMap();
            CreateMap<Carne, CarneCreateResult>();
        }
    }
}
