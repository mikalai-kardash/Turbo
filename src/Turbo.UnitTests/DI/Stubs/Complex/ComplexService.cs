using System;
using Turbo.UnitTests.DI.Stubs.Simple;

namespace Turbo.UnitTests.DI.Stubs.Complex
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