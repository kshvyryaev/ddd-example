using System;
using System.Linq.Expressions;
using DddExample.Domain.Aggregates;

namespace DddExample.Application.Specifications
{
    public class AndSpecification<TModel> : SpecificationBase<TModel>
        where TModel : class
    {
        private readonly ISpecification<TModel> _left;
        private readonly ISpecification<TModel> _right;

        public AndSpecification(ISpecification<TModel> left, ISpecification<TModel> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<TModel, bool>> ToExpression()
        {
            var left = _left.ToExpression();
            var right = _right.ToExpression();
            var parameter = Expression.Parameter(typeof(TModel));
            var body = Expression.AndAlso(left.Body, right.Body);
            body = (BinaryExpression)new ParameterReplacer(parameter).Visit(body);
            var result = Expression.Lambda<Func<TModel, bool>>(body, parameter);

            return result;
        }
    }
}