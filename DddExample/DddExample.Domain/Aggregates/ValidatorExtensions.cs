using System;
using FluentValidation;
using FluentValidation.Internal;

namespace DddExample.Domain.Aggregates
{
    internal static class ValidatorExtensions
    {
        public static void ValidateEntityAndThrow<TEntity>(
            this IValidator<TEntity> validator,
            TEntity entity,
            Action<ValidationStrategy<TEntity>> options = null)
        {
            var result = options == null
                ? validator.Validate(entity)
                : validator.Validate(entity, options);

            if (!result.IsValid)
            {
                throw new Exceptions.ValidationException(result);
            }
        }
    }
}