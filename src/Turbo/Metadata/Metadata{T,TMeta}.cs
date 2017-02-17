namespace Turbo.Metadata
{
    public class Metadata<T, TMeta> : Metadata<TMeta>
    {
        protected Metadata(TMeta meta)
            : base(typeof(T), meta)
        {
        }
    }
}