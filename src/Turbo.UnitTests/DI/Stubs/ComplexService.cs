using System;

namespace Turbo.UnitTests.DI.Stubs
{
    public class ComplexService : IComplexService
    {
        private readonly ISimpleService _simple;

        public ComplexService(ISimpleService simple)
        {
            if (simple == null)
            {
                throw new ArgumentNullException(nameof(simple));
            }
            _simple = simple;
        }

        public void Peek()
        {
            _simple.Run();
        }
    }
}