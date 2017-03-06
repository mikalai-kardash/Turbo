using System;

namespace Turbo.DI
{
    public sealed class TypeId
    {
        private TypeId(Type type, string name)
        {
            Type = type;
            Name = name;
        }

        private TypeId(Type type)
        {
            Type = type;
            Name = string.Empty;
        }

        public Type Type { get; }
        public string Name { get; }

        #region Factory Methods

        public static TypeId Unnamed(Type type)
        {
            return new TypeId(type);
        }

        public static TypeId Create(Type type, string name)
        {
            return new TypeId(type, name);
        }

        #endregion


        #region ToString + Equals + GetHashCode

        public override string ToString()
        {
            return $"{Name}:{Type.Name}({Type})";
        }

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