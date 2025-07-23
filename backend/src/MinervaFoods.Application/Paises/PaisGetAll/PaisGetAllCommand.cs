using MediatR;
using MinervaFoods.Application.Paises.Common;

namespace MinervaFoods.Application.Paises.PaisGetAll
{
    /// <summary>
    /// Comando para recuperar todos os países.
    /// </summary>
    /// <remarks>
    /// Este comando não possui parâmetros, pois busca todos os registros disponíveis de países.
    /// </remarks>
    public class PaisGetAllCommand : IRequest<IEnumerable<PaisResult>>
    {
    }
}
