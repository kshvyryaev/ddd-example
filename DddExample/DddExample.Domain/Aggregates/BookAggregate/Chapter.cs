namespace DddExample.Domain.Aggregates.BookAggregate
{
    public class Chapter : Entity<int>
    {
        #region Constructors

        protected Chapter(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Chapter NewChapter(string name, string description)
        {
            var chapter = new Chapter(name, description);
            return chapter;
        }
        
        #endregion Constructors

        #region Properties

        public string Name { get; private set; }

        public string Description { get; private set; }
        
        #endregion Properties

        #region Methods

        public void SetName(string name)
        {
            Name = name;
        }
        
        public void SetDescription(string description)
        {
            Description = description;
        }
        
        #endregion Methods
    }
}