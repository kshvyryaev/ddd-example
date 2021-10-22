using System.Threading;
using System.Threading.Tasks;
using DddExample.Domain.Events.Books;
using MediatR;

namespace DddExample.Application.DomainEventHandlers.Books
{
    public class BookCreatedPreEventHandler : INotificationHandler<BookCreatedPreEvent>
    {
        public Task Handle(BookCreatedPreEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Здесь можно прописать кукую-то логику
            
            return Task.CompletedTask;
        }
    }
}