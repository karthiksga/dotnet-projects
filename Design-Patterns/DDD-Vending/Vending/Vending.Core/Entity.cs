using System;
using System.Collections.Generic;
using System.Text;

namespace Vending.Core
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        public override bool Equals(object obj)
        {
            var other = obj as Entity;

            if (ReferenceEquals(other, null))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (this.GetType() != other.GetType())
                return false;
            if (Id == Guid.Empty || other.Id == Guid.Empty)
                return false;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }
    }
}
