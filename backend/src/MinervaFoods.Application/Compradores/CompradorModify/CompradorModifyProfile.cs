using AutoMapper;
using MinervaFoods.Application.Compradores.Common;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.Compradores.CompradorModify
{
    /// <summary>
    /// Profile para mapeamento entre entidades de Comprador e seus DTOs relacionados à modificação de compradores.
    /// </summary>
    public class CompradorModifyProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos utilizados na operação de modificação de compradores.
        /// </summary>
        public CompradorModifyProfile()
        {
            CreateMap<CompradorModifyCommand, Comprador>().ReverseMap();
            CreateMap<Comprador, CompradorResult>();
        }
    }
}
