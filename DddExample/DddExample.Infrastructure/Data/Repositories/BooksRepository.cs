using System.Linq;
using DddExample.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;

namespace DddExample.Infrastructure.Data.Repositories
{
    public class BooksRepository : RepositoryBase<Book, int>, IBooksRepository
    {
        public BooksRepository(DatabaseContext context) : base(context)
        {
        }

        protected override IQueryable<Book> Set => base.Set
            .Include(x => x.Chapters);

        public override Book Create(Book book)
        {
            Context.Books.Add(book);
            return book;
        }

        public override Book Update(Book book)
        {
            Context.Books.Update(book);
            return book;
        }
    }
}