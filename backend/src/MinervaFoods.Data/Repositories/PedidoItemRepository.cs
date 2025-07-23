using MinervaFoods.Data.Repositories.Common;

namespace MinervaFoods.Data.Repositories
{
    public class PedidoItemRepository : Repository<Domain.Entities.PedidoItem>, Domain.Repositories.IPedidoItemRepository
    {
        /// <summary>
        /// Initializes a new instance of CommentRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public PedidoItemRepository(DefaultContext context) : base(context)
        {
        }
    }
}
