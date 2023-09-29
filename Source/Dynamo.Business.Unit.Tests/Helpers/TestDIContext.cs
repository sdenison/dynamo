using System;

namespace Dynamo.Business.Unit.Tests.Helpers
{
    public class TestDIContext
    {
        public TestDIContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; private set; }
    }
}
