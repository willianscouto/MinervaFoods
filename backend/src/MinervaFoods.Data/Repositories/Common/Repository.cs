using Microsoft.EntityFrameworkCore;
using MinervaFoods.Domain.Common;
using MinervaFoods.Domain.Repositories.Common;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace MinervaFoods.Data.Repositories.Common
{
    public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, new()
    {
        #region Fields
        private readonly DefaultContext _context;
        public readonly DbSet<TEntity> _DbSet;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Repository
        /// </summary>
        /// <param name="context">The database context</param>
        public Repository(DefaultContext context)
        {
            _context = context;
            _DbSet = _context.Set<TEntity>();

        }
        #endregion

        #region CRUD Methods

        /// <summary>
        /// Creates a new entity in the database
        /// </summary>
        /// <param name="entity">The enity to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created entity</returns>
        public async System.Threading.Tasks.Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _DbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// Creates a new entity in the database
        /// </summary>
        /// <param name="entity">The enity to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created entity</returns>
        public async System.Threading.Tasks.Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _DbSet.AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entities;
        }


        /// <summary>
        /// Deletes an entity from the database by its unique identifier
        /// </summary>
        /// <param name="cancellationToken",>Cancellation token</param>
        /// <param name="id">The unique identifier of the entity</param>
        /// <returns>True if the entity was deleted, false if it was not found</returns>
        public async System.Threading.Tasks.Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {


            if (entity is BaseEntity baseEntity)
            {
                baseEntity.Status = false;

                _context.ChangeTracker.Clear();
                _context.Entry(baseEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }

        /// <summary>
        /// Deletes an entity from the database by its unique identifier
        /// </summary>
        /// <param name="cancellationToken",>Cancellation token</param>
        /// <param name="id">The unique identifier of the entity</param>
        /// <returns>True if the entity was deleted, false if it was not found</returns>
        public async System.Threading.Tasks.Task<bool> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {


            foreach (var entity in entities)
            {

                if (entity is BaseEntity baseEntity)
                {
                    baseEntity.Status = false;

                    _context.ChangeTracker.Clear();
                    _context.Entry(baseEntity).State = EntityState.Modified;
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    _context.Remove(entity);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            return true;
        }

        /// <summary>
        /// Updates an existing entity in the database
        /// </summary>  
        /// <param name="entity"> The entity to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated entity</returns>
        public async System.Threading.Tasks.Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _context.ChangeTracker.Clear();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// Updates an existing entity in the database
        /// </summary>  
        /// <param name="entity"> The entity to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated entity</returns>
        public async System.Threading.Tasks.Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entites, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entites)
            {
                _context.ChangeTracker.Clear();
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync(cancellationToken);
            }

            return entites;
        }
        #endregion

        #region READ Methods

        /// <summary>
        ///  Query the database for entities that match a given predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includes)
        {
            IQueryable<TEntity> query = ApplyIncludes(includes);
            query = FilterByStatus(query);
            return await query.Where(predicate).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Query the database for all entities of type TEntity
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A collection of entities</returns>
        public async System.Threading.Tasks.Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, params string[] includes)
        {
            IQueryable<TEntity> query = ApplyIncludes(includes);
            var entities = await FilterByStatus(query).ToListAsync(cancellationToken);
            FilterChildEntities(entities);

            return entities;
        }

        /// <summary>
        /// Query the database for an entity by its unique identifier
        /// </summary>  
        /// <params name="cancellationToken">Cancellation token</param>
        /// <params name="id">The unique identifier of the entity</params>
        /// <returns>The entity if found, null otherwise</returns>
        public async System.Threading.Tasks.Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, params string[] includes)
        {
            IQueryable<TEntity> query = ApplyIncludes(includes);
            var entity = await FilterByStatusAndId(query, [id])
                .FirstOrDefaultAsync(cancellationToken);

            FilterChildEntities(entity);

            return entity!;
        }

        /// <summary>
        /// Query the database for an entity by its unique identifier
        /// </summary>  
        /// <params name="cancellationToken">Cancellation token</param>
        /// <params name="id">The unique identifier of the entity</params>
        /// <returns>The entity if found, null otherwise</returns>
        public async System.Threading.Tasks.Task<IEnumerable<TEntity>> GetByIdAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default, params string[] includes)
        {
            IQueryable<TEntity> query = ApplyIncludes(includes);
            var entities = await FilterByStatusAndId(query, ids)
                .ToListAsync(cancellationToken);

            FilterChildEntities(entities);

            return entities!;
        }

        #endregion

        #region Private Methods
        private static IQueryable<TEntity> FilterByStatus(IQueryable<TEntity> query)
        {
            if (typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            {
                var parameter = Expression.Parameter(typeof(TEntity), "entity");
                var cast = Expression.Convert(parameter, typeof(BaseEntity));
                var statusProperty = Expression.Property(cast, nameof(BaseEntity.Status));
                var lambda = Expression.Lambda<Func<TEntity, bool>>(statusProperty, parameter);
                return query.Where(lambda);
            }
            return query;
        }

        private static IQueryable<TEntity> FilterByStatusAndId(IQueryable<TEntity> query, IEnumerable<Guid> ids)
        {
            if (typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            {
                return query.Where(entity => ((BaseEntity)(object)entity).Status && ids.Contains(((BaseEntity)(object)entity).Id));
            }
            return query;
        }



        private IQueryable<TEntity> ApplyIncludes(params string[] includes)
        {
            IQueryable<TEntity> query = _DbSet.AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }

        private static void FilterChildEntities<T>(T entity)
        {
            if (entity == null) return;

            var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
             .Where(p => p.CanRead && p.CanWrite && p.GetIndexParameters().Length == 0);

            foreach (var prop in properties)
            {
                var propValue = prop.GetValue(entity);

                if (propValue == null || propValue is string)
                    continue;

                // Se for uma coleção (ex: lista de filhos)
                if (propValue is IEnumerable enumerable)
                {
                    var elementType = GetElementType(prop.PropertyType);
                    if (elementType == null)
                        continue;

                    var filteredItems = new List<object>();

                    foreach (var item in enumerable)
                    {
                        if (item is BaseEntity baseEntity && baseEntity.Status)
                        {
                            FilterChildEntities(baseEntity);
                            filteredItems.Add(baseEntity);
                        }
                    }

                    var typedList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType))!;
                    foreach (var item in filteredItems)
                    {
                        typedList.Add(item);
                    }

                    prop.SetValue(entity, typedList);
                }
                else if (propValue is BaseEntity baseEntity)
                {
                    if (baseEntity.Status)
                    {
                        FilterChildEntities(baseEntity);
                    }
                    else
                    {
                        prop.SetValue(entity, null);
                    }
                }
            }
        }


        private static void FilterChildEntities<T>(IEnumerable<T> entities)
        {
            if (entities == null) return;

            foreach (var entity in entities)
            {
                FilterChildEntities(entity);
            }
        }


        private static Type? GetElementType(Type collectionType)
        {
            if (collectionType.IsArray)
                return collectionType.GetElementType();

            if (collectionType.IsGenericType)
            {
                return collectionType.GetGenericArguments().FirstOrDefault();
            }


            return null;
        }
        #endregion

    }
}
