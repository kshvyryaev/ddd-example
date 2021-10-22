using System;
using System.Linq.Expressions;

namespace DddExample.Domain.Aggregates
{
    public interface ISpecification<TModel>
    {
        Expression<Func<TModel, bool>> ToExpression();

        ISpecification<TModel> And(ISpecification<TModel> specification);

        ISpecification<TModel> Or(ISpecification<TModel> specification);
    }
}