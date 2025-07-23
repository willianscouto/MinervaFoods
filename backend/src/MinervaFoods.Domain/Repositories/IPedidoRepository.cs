using MinervaFoods.Domain.Entities;
using MinervaFoods.Domain.Repositories.Common;

namespace MinervaFoods.Domain.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<long> GetProximoNumeroPedidoAsync(CancellationToken cancellationToken);
    }
}
