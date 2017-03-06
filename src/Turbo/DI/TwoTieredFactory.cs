using System;

namespace Turbo.DI
{
    public class TwoTieredFactory : ObjectFactoryTemplate
    {
        private readonly IFactoryAlgorithm _global;
        private readonly IFactoryAlgorithm _local;

        public TwoTieredFactory(IFactoryAlgorithm global, IFactoryAlgorithm local)
        {
            if (global == null)
            {
                throw new ArgumentNullException(nameof(global));
            }
            if (local == null)
            {
                throw new ArgumentNullException(nameof(local));
            }

            _global = global;
            _local = local;
        }

        public override void Dispose()
        {
        }

        public override object GetInstance(TypeId id)
        {
            return _local.GetInstance(id) ?? _global.GetInstance(id);
        }

        public override object CreateInstance(TypeId id)
        {
            return _local.CreateInstance(id) ?? _global.CreateInstance(id);
        }
    }
}