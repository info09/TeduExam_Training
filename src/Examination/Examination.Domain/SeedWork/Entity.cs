using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Examination.Domain.SeedWork
{
    public abstract class Entity
    {
        private int? _requestedHashCode;

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        private List<INotification> _domainEvents;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDmainEvent()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return this.Id == default;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            Entity entity = (Entity)obj;
            if (entity.IsTransient() || this.IsTransient())
                return false;
            else
                return entity.Id == this.Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value; 
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public static bool operator == (Entity left, Entity right)
        {
            if(Object.Equals(left, null))
                return (Object.Equals(right, null) ? true : false);
            else
                return left.Equals(right);
        }

        public static bool operator != (Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
