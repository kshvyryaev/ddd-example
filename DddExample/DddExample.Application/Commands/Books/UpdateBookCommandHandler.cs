using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DddExample.Application.Specifications;
using DddExample.Domain.Aggregates;
using DddExample.Domain.Aggregates.BookAggregate;
using DddExample.Domain.Exceptions;
using MediatR;

namespace DddExample.Application.Commands.Books
{
    public class UpdateBookCommandHandler : AsyncRequestHandler<UpdateBookCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        protected override async Task Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.FindFirstAsync(new BookById(command.Id));

            if (book == null)
                throw new NotFoundException(nameof(Book), command.Id);
            
            book.SetName(command.Name);
            book.SetDescription(command.Description);
            book.SetType(command.TypeId);
            HandleChapters(book, command);

            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveChangesInTransactionAsync();
        }

        private static void HandleChapters(Book book, UpdateBookCommand command)
        {
            if (command.Chapters.Any())
            {
                var advantagesToAdd = command.Chapters
                    .Where(x => x.Id == default)
                    .ToList();

                var advantagesToUpdate = command.Chapters
                    .Where(x => book.Chapters.Any(y => y.Id == x.Id && !y.IsDeleted))
                    .ToList();

                var advantagesToDelete = book.Chapters
                    .Where(x => command.Chapters.All(y => x.Id != y.Id) && !x.IsDeleted)
                    .ToList();

                foreach (var advantage in advantagesToAdd)
                    book.AddChapter(advantage.Name, advantage.Description);

                foreach (var advantage in advantagesToUpdate)
                    book.UpdateChapter(advantage.Id, advantage.Name, advantage.Description);

                foreach (var advantage in advantagesToDelete)
                    book.DeleteChapterById(advantage.Id);
            }
            else
            {
                book.DeleteAllChapters();
            }
        }
    }
}