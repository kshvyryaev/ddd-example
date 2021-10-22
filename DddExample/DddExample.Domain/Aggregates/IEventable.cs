using System.Collections.Generic;
using MediatR;

namespace DddExample.Domain.Aggregates
{
    public interface IEventable
    {
        IReadOnlyCollection<INotification> PreDomainEvents { get; }
        
        IReadOnlyCollection<INotification> PostDomainEvents { get; }
        
        void AddPreDomainEvent(INotification @event);

        void RemovePreDomainEvent(INotification @event);

        void ClearPreDomainEvents();

        void AddPostDomainEvent(INotification @event);

        void RemovePostDomainEvent(INotification @event);

        void ClearPostDomainEvents();
    }
}