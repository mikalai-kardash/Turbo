using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Turbo.DI
{
    public class TypeCache : ITypeCache
    {
        private readonly ConcurrentDictionary<TypeId, Registration> _registrations
            = new ConcurrentDictionary<TypeId, Registration>();

        public void Add(Registration registration)
        {
            _registrations.AddOrUpdate(
                registration.Id,
                registration,
                (id, r) => registration);
        }

        public IEnumerable<Registration> All => _registrations.Values;

        public Registration Find(TypeId typeId)
        {
            Registration r;
            if (_registrations.TryGetValue(typeId, out r))
            {
                return r;
            }

            foreach (var registration in _registrations.Values)
            {
                if (registration.IsRelatedTo(typeId))
                {
                    r = registration;
                    break;
                }
            }

            return r;
        }
    }
}