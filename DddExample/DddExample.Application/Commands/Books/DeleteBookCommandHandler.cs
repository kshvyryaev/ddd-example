using System.Threading;
using System.Threading.Tasks;
using DddExample.Application.Specifications;
using DddExample.Domain.Aggregates;
using DddExample.Domain.Aggregates.BookAggregate;
using DddExample.Domain.Exceptions;
using MediatR;

namespace DddExample.Application.Commands.Books
{
    public class DeleteBookCommandHandler : AsyncRequestHandler<DeleteBookCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        protected override async Task Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.FindFirstAsync(new BookById(command.Id));

            if (book == null)
                throw new NotFoundException(nameof(Book), command.Id);

            book.SetIsDeleted();

            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveChangesInTransactionAsync();
        }
    }
}