using System;

namespace DddExample.Domain.Aggregates
{
    public abstract class Enumeration : IComparable
    {
        #region Constructors

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        #endregion Constructors
        
        #region Properties

        public int Id { get; protected set; }
        
        public string Name { get; protected set; }

        #endregion Properties

        #region Methods

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();
        
        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
        
        #endregion Methods
    }
}
