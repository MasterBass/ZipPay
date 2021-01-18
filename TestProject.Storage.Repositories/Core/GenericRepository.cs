using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProject.Common.Extensions;
using TestProject.Common.Filtering;
using TestProject.Storage.Core;
using TestProject.Storage.DAL.Exceptions;

namespace TestProject.Storage.Repositories.Core
{
    public class GenericRepository<TContext, TEntity, TKey> : IGenericRepository<TContext, TEntity, TKey>
       where TContext : DbContext
       where TEntity : class, IEntity<TKey>
       where TKey : IEquatable<TKey>
    {
        protected readonly TContext Context;

        protected GenericRepository(TContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAll(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var result = QueryDb(null, orderBy, null, includeProperties);
            return await result.ToListAsync();
        }

        public async Task<TEntity> GetById(TKey id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var result = QueryDb(null, null, null, includeProperties);
            return await result
                .SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<TEntity> Find(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var result = QueryDb(filter, null, null, includeProperties);
            return await result
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? takeCount = null,
            int? skipCount = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var result = QueryDb(filter, orderBy, null, includeProperties);

            if (skipCount.HasValue)
            {
                result = result.Skip(skipCount.Value);
            }

            if (takeCount != null)
            {
                result = result.Take(takeCount.Value);
            }

            return await result.ToListAsync();
        }

        public async Task<PagedEntityResult<TEntity>> PagedQuery(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            PageInfoModel pageInfo = null,
            params Expression<Func<TEntity, object>>[] includeProperties
            )
        {
            var query = QueryDb(filter, orderBy, pageInfo, includeProperties);

            int? skipCount = null;

            if (pageInfo != null && pageInfo.PageNumber.HasValue && pageInfo.PageSize.HasValue)
            {
                skipCount = pageInfo.PageNumber * pageInfo.PageSize;
            }

            var pagedEntity = new PagedEntityResult<TEntity>(query.AsQueryable(), 
                new PageInfoModel
                {
                    PageSize = pageInfo?.PageSize.GetValueOrDefault(), 
                    PageNumber = pageInfo?.PageNumber.GetValueOrDefault(),
                    SortKey = pageInfo?.SortKey,
                    SortDir = pageInfo?.SortDir
                });

            if (skipCount.HasValue)
            {
                query = query.Skip(skipCount.Value);
            }

            if (pageInfo != null && pageInfo.PageSize.HasValue)
            {
                query = query.Take(pageInfo.PageSize.Value);
            }

            pagedEntity.Result = await query.AsNoTracking().ToListAsync();

            return pagedEntity;
        }

        public virtual async Task Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException("Unable to add a null entity to the repository.");
            }

            await Context.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task Update(TKey id, TEntity entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException("Unable to update a null entity to the repository.");
            }

            if (await GetById(id) == null)
            {
                throw new EntityNotFoundException<TKey>(typeof(TEntity).Name, id);
            }

            Context.Set<TEntity>().Update(entity);
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                throw new EntityNotFoundException<TKey>(typeof(TEntity).Name, id);
            }

            Context.Set<TEntity>().Remove(entity);
        }
        protected virtual IQueryable<TEntity> QueryDb(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            PageInfoModel pagedInfo = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context
                .Set<TEntity>()
                .AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var isAdditionalOrder =
               !string.IsNullOrEmpty(pagedInfo?.SortDir) && !string.IsNullOrEmpty(pagedInfo.SortKey);

            if (isAdditionalOrder)
            {
                query = pagedInfo.SortDir == "desc" ? query.SortDesc(pagedInfo.SortKey) : query.Sort(pagedInfo.SortKey);
            }
            return query;
        }
        
    }
}