namespace Turbo.DI
{
    internal class Module : DefaultObjectFactory
    {
        protected IObjectRegistry Registry => this;
    }
}