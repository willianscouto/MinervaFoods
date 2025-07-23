using MinervaFoods.Data.Repositories.Common;

namespace MinervaFoods.Data.Repositories
{
    public class CarneRepository : Repository<Domain.Entities.Carne>, Domain.Repositories.ICarneRepository
    {
        /// <summary>
        /// Initializes a new instance of CommentRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public CarneRepository(DefaultContext context) : base(context)
        {
        }
    }
}
