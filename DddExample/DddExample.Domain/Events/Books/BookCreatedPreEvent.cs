using DddExample.Domain.Aggregates.BookAggregate;
using MediatR;

namespace DddExample.Domain.Events.Books
{
    public class BookCreatedPreEvent : INotification
    {
        public BookCreatedPreEvent(Book book)
        {
            Book = book;
        }
        
        public Book Book { get; }
    }
}