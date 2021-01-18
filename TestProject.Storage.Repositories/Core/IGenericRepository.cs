using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestProject.Common.Filtering;
using TestProject.Storage.Core;

namespace TestProject.Storage.Repositories.Core
{
    public interface IGenericRepository<TContext, TEntity, in TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetById(TKey id, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> Find(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? takeCount = null,
            int? skipCount = null,
            params Expression<Func<TEntity, object>>[] includeProperties);
        
        Task<PagedEntityResult<TEntity>>  PagedQuery(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            PageInfoModel pageInfo = null,
            params Expression<Func<TEntity, object>>[] includeProperties
        );

        Task Create(TEntity entity);

        Task Update(TKey id, TEntity entity);

        Task DeleteAsync(TKey id);

    }
}