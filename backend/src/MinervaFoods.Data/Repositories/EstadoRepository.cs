using MinervaFoods.Data.Repositories.Common;

namespace MinervaFoods.Data.Repositories
{
    public class EstadoRepository : Repository<Domain.Entities.Estado>, Domain.Repositories.IEstadoRepository
    {
        /// <summary>
        /// Initializes a new instance of PaisRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public EstadoRepository(DefaultContext context) : base(context)
        {
        }
    }
}
