using System.Threading;
using System.Threading.Tasks;
using DddExample.Domain.Aggregates;
using DddExample.Domain.Aggregates.BookAggregate;
using MediatR;

namespace DddExample.Application.Commands.Books
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<int> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var book = Book.NewBook(command.Name, command.Description, command.TypeId);

            foreach (var chapter in command.Chapters)
                book.AddChapter(chapter.Name, chapter.Description);
            
            book.ValidateAndThrow();

            _unitOfWork.Books.Create(book);
            await _unitOfWork.SaveChangesInTransactionAsync();

            return book.Id;
        }
    }
}