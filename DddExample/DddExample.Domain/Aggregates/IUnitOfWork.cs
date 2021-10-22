using System;
using System.Threading.Tasks;
using DddExample.Domain.Aggregates.BookAggregate;

namespace DddExample.Domain.Aggregates
{
    public interface IUnitOfWork
    {
        IBooksRepository Books { get; }
        
        Task SaveChangesAsync();

        Task SaveChangesInTransactionAsync();

        Task ExecuteInTransactionAsync(Func<Task> func);
    }
}