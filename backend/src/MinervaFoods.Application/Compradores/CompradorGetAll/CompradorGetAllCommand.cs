using MediatR;
using MinervaFoods.Application.Carnes.Common;
using MinervaFoods.Application.Compradores.Common;

namespace MinervaFoods.Application.Compradores.CompradorGetAll
{
    /// <summary>
    /// Comando para recuperar todas as compradores.
    /// </summary>
    /// <remarks>
    /// Este comando não possui parâmetros, pois busca todos os registros disponíveis.
    /// </remarks>
    public class CompradorGetAllCommand : IRequest<IEnumerable<CompradorResult>>
    {
    }
}
