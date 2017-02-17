using System;

namespace Turbo.Metadata
{
    public class Metadata<TMeta>
    {
        protected Metadata(Type type, TMeta meta)
        {
            Type = type;
            Meta = meta;
        }

        public Type Type { get; }

        public TMeta Meta { get; }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        private bool Equals(Metadata<TMeta> other)
        {
            return Type == other.Type;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((Metadata<TMeta>)obj);
        }
    }
}