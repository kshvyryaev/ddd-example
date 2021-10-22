using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DddExample.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace DddExample.Infrastructure.Data.Repositories
{
    public abstract class RepositoryBase<TEntity, TId> : IRepository<TEntity>
        where TEntity : Entity<TId>, IAggregateRoot
        where TId : IEquatable<TId>
    {
        protected RepositoryBase(DatabaseContext context)
        {
            Context = context;
        }

        protected DatabaseContext Context { get; }

        protected virtual IQueryable<TEntity> Set => Context.Set<TEntity>();

        public virtual TEntity Create(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            return entity;
        }
        
        public virtual TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public Task<TEntity> FindFirstAsync(ISpecification<TEntity> specification, bool isTrackingEnabled = false)
        {
            var query = Set;
            if (!isTrackingEnabled) query.AsNoTracking();
            return query.FirstOrDefaultAsync(specification.ToExpression());
        }

        public Task<TEntity> FindLastAsync(ISpecification<TEntity> specification, bool isTrackingEnabled = false)
        {
            var query = Set;
            if (!isTrackingEnabled) query.AsNoTracking();
            return query.OrderBy(x => x.Id).LastOrDefaultAsync(specification.ToExpression());
        }

        public Task<List<TEntity>> FindManyAsync(ISpecification<TEntity> specification, bool isTrackingEnabled = false)
        {
            var query = Set;
            if (!isTrackingEnabled) query.AsNoTracking();
            return query.Where(specification.ToExpression()).ToListAsync();
        }

        public Task<bool> AnyAsync(ISpecification<TEntity> specification) => Set
            .AsNoTracking()
            .AnyAsync(specification.ToExpression());

        public async Task DeleteAsync(ISpecification<TEntity> specification)
        {
            var entities = await Set.Where(specification.ToExpression()).ToListAsync();
            entities.ForEach(entity => Context.Entry(entity).State = EntityState.Deleted);
        }
    }
}