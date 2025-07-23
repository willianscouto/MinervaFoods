using MediatR;
using MinervaFoods.Application.Estados.Common;

namespace MinervaFoods.Application.Estados.EstadoGetAll
{
    /// <summary>
    /// Comando para recuperar todos os estados.
    /// </summary>
    /// <remarks>
    /// Este comando não possui parâmetros, pois busca todos os registros disponíveis.
    /// </remarks>
    public class EstadoGetAllCommand : IRequest<IEnumerable<EstadoResult>>
    {
    }
}
