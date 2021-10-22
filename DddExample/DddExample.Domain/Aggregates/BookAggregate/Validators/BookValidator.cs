using FluentValidation;

namespace DddExample.Domain.Aggregates.BookAggregate.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Description).MaximumLength(4000);
            RuleForEach(x => x.Chapters).SetValidator(new ChapterValidator());
        }
    }
}