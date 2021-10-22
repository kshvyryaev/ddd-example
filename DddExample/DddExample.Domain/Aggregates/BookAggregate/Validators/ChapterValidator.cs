using FluentValidation;

namespace DddExample.Domain.Aggregates.BookAggregate.Validators
{
    public class ChapterValidator : AbstractValidator<Chapter>
    {
        public ChapterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Description).MaximumLength(4000);
        }
    }
}