using AutoMapper;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.Compradores.CompradorCreate
{
    /// <summary>
    /// Profile do AutoMapper para mapeamento entre entidades de Comprador e os DTOs relacionados à criação de comprador.
    /// </summary>
    public class CompradorCreateProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos para a operação de criação de comprador.
        /// </summary>
        public CompradorCreateProfile()
        {
            CreateMap<CompradorCreateCommand, Comprador>().ReverseMap();
            CreateMap<Comprador, CompradorCreateResult>();
        }
    }
}
