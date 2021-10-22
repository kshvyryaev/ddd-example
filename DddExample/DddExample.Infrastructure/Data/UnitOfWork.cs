using System;
using System.Threading.Tasks;
using DddExample.Domain.Aggregates;
using DddExample.Domain.Aggregates.BookAggregate;
using MediatR;

namespace DddExample.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private readonly IMediator _mediator;

        public UnitOfWork(
            DatabaseContext context,
            IMediator mediator,
            IBooksRepository booksRepository)
        {
            _context = context;
            _mediator = mediator;
            Books = booksRepository;
        }

        public IBooksRepository Books { get; }

        public async Task SaveChangesAsync()
        {
            await _mediator.DispatchPreDomainEventsAsync(_context);
            await _context.SaveChangesAsync();
            await _mediator.DispatchPostDomainEventsAsync(_context);
        }
        
        public async Task SaveChangesInTransactionAsync()
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _mediator.DispatchPreDomainEventsAsync(_context);
                await _context.SaveChangesAsync();
                await _mediator.DispatchPostDomainEventsAsync(_context);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        
        public async Task ExecuteInTransactionAsync(Func<Task> func)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _mediator.DispatchPreDomainEventsAsync(_context);
                await func();
                await _mediator.DispatchPostDomainEventsAsync(_context);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}