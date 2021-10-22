using System;
using System.Linq.Expressions;
using DddExample.Domain.Aggregates.BookAggregate;

namespace DddExample.Application.Specifications
{
    public class BookById : SpecificationBase<Book>
    {
        private readonly int _id;

        public BookById(int id)
        {
            _id = id;
        }

        public override Expression<Func<Book, bool>> ToExpression() => x => x.Id == _id;
    }
}