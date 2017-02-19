namespace Turbo.Cache
{
    public interface ISelectorFinder
    {
        string GetSelector(string fieldName);
    }
}