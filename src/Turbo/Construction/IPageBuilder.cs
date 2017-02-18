using System.Reflection;

namespace Turbo.Construction
{
    public interface IPageBuilder
    {
        void SetWebElement(FieldInfo field);
        void SetWebElement(PropertyInfo property);
        void SetWebDriver(FieldInfo field);
        void SetPart(PropertyInfo property);
        void SetPart(FieldInfo field);
        void SetPartCollection(FieldInfo field);
    }
}