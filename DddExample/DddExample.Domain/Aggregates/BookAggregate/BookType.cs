using System;
using System.Collections.Generic;
using System.Linq;
using DddExample.Domain.Exceptions;

namespace DddExample.Domain.Aggregates.BookAggregate
{
    public class BookType : Enumeration
    {
        public static readonly BookType TechnicalLiterature = new(1, "Техническая литератуа");
        public static readonly BookType Other = new(2, "Другое");
        
        public BookType(int id, string name) : base(id, name)
        {
        }

        #region Methods

        public static IEnumerable<BookType> List() => new[]
        {
            TechnicalLiterature,
            Other
        };

        public static BookType FromId(int id)
        {
            var value = List()
                .FirstOrDefault(s => s.Id == id);

            if (value == null)
            {
                var possibleValues = string.Join(", ",  List().Select(s => $"[{s.Id}] {s.Name}"));
                throw new DomainException($"Возможные значения для {nameof(BookType)}: {possibleValues}");
            }

            return value;
        }
        
        public static BookType FromName(string name)
        {
            var value = List()
                .FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (value == null)
            {
                var possibleValues = string.Join(", ",  List().Select(s => $"[{s.Id}] {s.Name}"));
                throw new DomainException($"Возможные значения для {nameof(BookType)}: {possibleValues}");
            }

            return value;
        }
        
        #endregion Methods
    }
}