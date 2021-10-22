using System.Collections.Generic;
using System.Threading.Tasks;

namespace DddExample.Domain.Aggregates
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        Task<TEntity> FindFirstAsync(ISpecification<TEntity> specification, bool isTrackingEnabled = false);
        
        Task<TEntity> FindLastAsync(ISpecification<TEntity> specification, bool isTrackingEnabled = false);
        
        Task<List<TEntity>> FindManyAsync(ISpecification<TEntity> specification, bool isTrackingEnabled = false);
        
        Task<bool> AnyAsync(ISpecification<TEntity> specification);

        Task DeleteAsync(ISpecification<TEntity> specification);
    }
}