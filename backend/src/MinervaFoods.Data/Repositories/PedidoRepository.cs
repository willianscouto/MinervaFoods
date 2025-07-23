using Microsoft.EntityFrameworkCore;
using MinervaFoods.Data.Repositories.Common;

namespace MinervaFoods.Data.Repositories
{
    public class PedidoRepository : Repository<Domain.Entities.Pedido>, Domain.Repositories.IPedidoRepository
    {
        /// <summary>
        /// Initializes a new instance of CommentRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public PedidoRepository(DefaultContext context) : base(context)
        {
        }

        public async Task<long> GetProximoNumeroPedidoAsync(CancellationToken cancellationToken)
        {
            var numbers = await _DbSet
                                .AsNoTracking()
                                .Select(s => s.NumeroPedido)
                                .ToListAsync(cancellationToken);

            return (numbers.Any() ? numbers.Max() : 0) + 1;

        }
    }
}
