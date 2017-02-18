namespace Turbo.Construction
{
    public interface IPageFactory
    {
        TPage Build<TPage>();
    }
}