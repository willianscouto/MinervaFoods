using MediatR;
using MinervaFoods.Application.Pedidos.Common;

namespace MinervaFoods.Application.Pedidos.PedidoGetAll
{
    /// <summary>
    /// Comando para recuperar todas as compradores.
    /// </summary>
    /// <remarks>
    /// Este comando não possui parâmetros, pois busca todos os registros disponíveis.
    /// </remarks>
    public class PedidoGetAllCommand : IRequest<IEnumerable<PedidoResult>>
    {
    }
}
