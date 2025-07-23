using MinervaFoods.Data.Repositories.Common;

namespace MinervaFoods.Data.Repositories
{
    public class CompradorRepository : Repository<Domain.Entities.Comprador>, Domain.Repositories.ICompradorRepository
    {
        /// <summary>
        /// Initializes a new instance of CommentRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public CompradorRepository(DefaultContext context) : base(context)
        {
        }
    }
}
