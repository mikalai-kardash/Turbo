namespace Turbo.DI
{
    public class Module : SimpleObjectFactory
    {
        protected Module()
        {
        }

        protected IObjectFactoryRegistry Registry => this;

        protected void Include<T>() where T : Module, new()
        {
            var module = new T();

            CopyTypes(module);
            CopyObjects(module);
        }

        private void CopyTypes<T>(T module) where T : Module, new()
        {
            var from = module.GetInstance<ITypeCache>();
            var to = this.GetInstance<ITypeCache>();

            foreach (var registration in from.All)
            {
                to.Add(registration);
            }
        }

        private void CopyObjects<T>(T module) where T : Module, new()
        {
            var from = module.GetInstance<IObjectCache>();
            var to = this.GetInstance<IObjectCache>();

            foreach (var id in from.AllObjectIds)
            {
                if (id == InternalTypeIds.DefaultObjectFactory) continue;
                if (id == InternalTypeIds.ObjectRegistry) continue;
                if (id == InternalTypeIds.ObjectFactory) continue;
                if (id == InternalTypeIds.ObjectCache) continue;
                if (id == InternalTypeIds.TypeCache) continue;

                var i = module.GetInstance(id.Type, id.Name);
                to.Add(id, i);
            }
        }
    }
}