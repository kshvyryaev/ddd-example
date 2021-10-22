using MediatR;

namespace DddExample.Application.Commands.Books
{
    public class DeleteBookCommand : IRequest
    {
        public DeleteBookCommand(int id)
        {
            Id = id;
        }
        
        public int Id { get; }
    }
}