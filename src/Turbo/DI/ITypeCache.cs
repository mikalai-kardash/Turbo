using System.Collections.Generic;

namespace Turbo.DI
{
    public interface ITypeCache
    {
        IEnumerable<Registration> All { get; }

        void Add(Registration registration);
        Registration Find(TypeId typeId);
    }
}