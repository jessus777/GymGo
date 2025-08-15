using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymGo.Domain.Common
{
    public abstract class Entity
        : IEquatable<Entity>
    {
        protected Entity(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private init; }

        public static bool operator ==(Entity first, Entity second)
        {
            if (ReferenceEquals(first, second))
                return true;
            if (first is null || second is null)
                return false;
            return first.Equals(second);
        }

        public static bool operator !=(Entity first, Entity second)
        {
            return !(first == second);
        }

        public bool Equals(Entity other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (other.GetType() != GetType())
                return false;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 41;
        }
    }
}
