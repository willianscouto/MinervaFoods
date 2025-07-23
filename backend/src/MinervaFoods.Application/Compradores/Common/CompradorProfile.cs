using AutoMapper;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Application.Compradores.Common
{
    /// <summary>
    /// Profile de mapeamento entre a entidade Comprador e o modelo CompradorResult.
    /// </summary>
    public class CompradorProfile : Profile
    {
        /// <summary>
        /// Inicializa os mapeamentos para a entidade Comprador.
        /// </summary>
        public CompradorProfile()
        {
            CreateMap<Comprador, CompradorResult>();
        }
    }
}
