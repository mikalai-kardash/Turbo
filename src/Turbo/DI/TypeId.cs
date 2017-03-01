using System;

namespace Turbo.DI
{
    public sealed class TypeId
    {
        public TypeId(Type type, string name)
        {
            Type = type;
            Name = name;
        }

        public Type Type { get; }
        public string Name { get; }

        public override string ToString()
        {
            return $"{Type.FullName}/{Name}";
        }

        #region Equals + GetHashCode

        protected bool Equals(TypeId other)
        {
            return Type == other.Type && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(TypeId)) return false;
            return Equals((TypeId) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Type.GetHashCode() * 397) ^ (Name?.GetHashCode() ?? 0);
            }
        }

        public static bool operator ==(TypeId left, TypeId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TypeId left, TypeId right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}