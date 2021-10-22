using System;
using System.Linq.Expressions;
using DddExample.Domain.Aggregates;

namespace DddExample.Application.Specifications
{
    public abstract class SpecificationBase<TModel> : ISpecification<TModel>
        where TModel : class
    {
        public abstract Expression<Func<TModel, bool>> ToExpression();

        public ISpecification<TModel> And(ISpecification<TModel> specification)
            => new AndSpecification<TModel>(this, specification);

        public ISpecification<TModel> Or(ISpecification<TModel> specification)
            => new OrSpecification<TModel>(this, specification);
    }
}