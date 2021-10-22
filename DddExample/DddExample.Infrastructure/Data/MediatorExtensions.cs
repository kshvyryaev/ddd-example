using System.Linq;
using System.Threading.Tasks;
using DddExample.Domain.Aggregates;
using MediatR;

namespace DddExample.Infrastructure.Data
{
    internal static class MediatorExtensions
    {
        public static async Task DispatchPreDomainEventsAsync(this IMediator mediator, DatabaseContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<IEventable>()
                .Where(x => x.Entity.PreDomainEvents != null && x.Entity.PreDomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.PreDomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearPreDomainEvents());

            foreach (var @event in domainEvents)
                await mediator.Publish(@event);
        }
        
        public static async Task DispatchPostDomainEventsAsync(this IMediator mediator, DatabaseContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<IEventable>()
                .Where(x => x.Entity.PostDomainEvents != null && x.Entity.PostDomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.PostDomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearPostDomainEvents());
            
            foreach (var @event in domainEvents)
                await mediator.Publish(@event);
        }
    }
}