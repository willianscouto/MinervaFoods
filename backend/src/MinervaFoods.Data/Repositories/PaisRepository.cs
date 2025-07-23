using MinervaFoods.Data.Repositories.Common;

namespace MinervaFoods.Data.Repositories
{
    public class PaisRepository : Repository<Domain.Entities.Pais>, Domain.Repositories.IPaisRepository
    {
        /// <summary>
        /// Initializes a new instance of PaisRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public PaisRepository(DefaultContext context) : base(context)
        {
        }
    }
}
