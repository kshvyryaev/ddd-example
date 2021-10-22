using System.Collections.Generic;
using System.Linq;
using DddExample.Domain.Aggregates.BookAggregate.Validators;
using DddExample.Domain.Events.Books;

namespace DddExample.Domain.Aggregates.BookAggregate
{
    public class Book : Entity<int>, IAggregateRoot
    {
        #region Fields

        private static BookValidator Validator = new();
        
        #endregion Fields
        
        #region Constructors

        protected Book(string name, string description, int typeId)
        {
            Name = name;
            Description = description;
            TypeId = typeId;
            _chapters = new List<Chapter>();
        }

        public static Book NewBook(string name, string description, int typeId)
        {
            var book = new Book(name, description, typeId);
            book.AddBookCreatedEvent();
            return book;
        }
        
        #endregion Constructors

        #region Properties

        public string Name { get; private set; }

        public string Description { get; private set; }

        public int TypeId { get; private set; }
        
        private readonly List<Chapter> _chapters;
        public IReadOnlyCollection<Chapter> Chapters => _chapters.AsReadOnly();
        
        #endregion Properties

        #region Book methods

        public BookType GetBookType() => BookType.FromId(TypeId);

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetType(int typeId)
        {
            TypeId = typeId;
        }

        public void ValidateAndThrow()
        {
            Validator.ValidateEntityAndThrow(this);
        }
        
        #endregion Book methods

        #region Chapters methods

        public void AddChapter(string name, string description)
        {
            _chapters.Add(Chapter.NewChapter(name, description));
        }
        
        public void UpdateChapter(int id, string name, string description)
        {
            var chapter = _chapters.FirstOrDefault(x => x.Id == id);

            if (chapter != null)
            {
                chapter.SetName(name);
                chapter.SetDescription(description);
                chapter.SetIsDeleted(false);
            }
        }

        public void DeleteChapterById(int id)
        {
            var chapter = _chapters.FirstOrDefault(x => x.Id == id);

            if (chapter != null)
                chapter.SetIsDeleted();
        }
        
        public void DeleteAllChapters()
        {
            _chapters.ForEach(x => x.SetIsDeleted());
        }

        #endregion Chapters methods

        #region Event methods

        private void AddBookCreatedEvent()
        {
            var @event = new BookCreatedPreEvent(this);
            AddPreDomainEvent(@event);
        }

        #endregion Event methods
    }
}