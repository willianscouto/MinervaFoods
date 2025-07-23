using MediatR;
using MinervaFoods.Application.Carnes.Common;
using MinervaFoods.Helpers;

namespace MinervaFoods.Application.Carnes.CarneGetAll
{
    /// <summary>
    /// Comando para recuperar todas as carnes.
    /// </summary>
    /// <remarks>
    /// Este comando não possui parâmetros, pois busca todos os registros disponíveis.
    /// </remarks>
    public class CarneGetAllCommand : IRequest<IEnumerable<CarneResult>>
    {
    }
}
