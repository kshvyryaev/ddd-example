using System;
using System.Collections.Generic;
using MediatR;

namespace DddExample.Domain.Aggregates
{
    public abstract class Entity<TId> : IEventable
        where TId : IEquatable<TId>
    {
        #region Constructors

        protected Entity()
        {
            IsDeleted = false;
            _preDomainEvents = new List<INotification>();
            _postDomainEvents = new List<INotification>();
        }

        #endregion Constructors

        #region Properties

        public TId Id { get; protected set; }

        public bool IsDeleted { get; protected set; }

        private List<INotification> _preDomainEvents;
        public IReadOnlyCollection<INotification> PreDomainEvents => _preDomainEvents.AsReadOnly();

        private List<INotification> _postDomainEvents;
        public IReadOnlyCollection<INotification> PostDomainEvents => _postDomainEvents.AsReadOnly();

        #endregion Properties

        #region Methods

        public virtual void SetIsDeleted(bool isDeleted = true)
        {
            IsDeleted = isDeleted;
        }

        public void AddPreDomainEvent(INotification @event)
        {
            _preDomainEvents.Add(@event);
        }

        public void RemovePreDomainEvent(INotification @event)
        {
            _preDomainEvents.Remove(@event);
        }

        public void ClearPreDomainEvents()
        {
            _preDomainEvents.Clear();
        }

        public void AddPostDomainEvent(INotification @event)
        {
            _postDomainEvents.Add(@event);
        }

        public void RemovePostDomainEvent(INotification @event)
        {
            _postDomainEvents.Remove(@event);
        }

        public void ClearPostDomainEvents()
        {
            _postDomainEvents.Clear();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Entity<TId> entity)
                return false;

            if (ReferenceEquals(this, entity))
                return true;

            if (GetType() != entity.GetType())
                return false;

            if (entity.IsTransient() || IsTransient())
                return false;

            return entity.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if (IsTransient())
            {
                return base.GetHashCode();
            }

            return Id.GetHashCode() ^ 31;
        }

        public bool IsTransient() => Id.Equals(default);

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            if (Object.Equals(left, null))
            {
                return Object.Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }

        #endregion Methods
    }
}