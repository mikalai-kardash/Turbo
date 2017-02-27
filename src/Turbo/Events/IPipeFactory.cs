namespace Turbo.Events
{
    public interface IPipeFactory
    {
        EventPipe CreatePipe(string name);
    }
}