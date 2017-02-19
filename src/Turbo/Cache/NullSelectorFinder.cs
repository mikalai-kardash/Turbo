namespace Turbo.Cache
{
    public class NullSelectorFinder : ISelectorFinder
    {
        public string GetSelector(string fieldName)
        {
            return string.Empty;
        }
    }
}