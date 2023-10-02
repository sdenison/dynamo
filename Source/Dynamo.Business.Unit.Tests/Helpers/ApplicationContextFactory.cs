using Microsoft.Extensions.DependencyInjection;
using Csla;

namespace Dynamo.Business.Unit.Tests.Helpers
{
    public static class ApplicationContextFactory
    {
        public static ApplicationContext CreateTestApplicationContext(TestDIContext context)
        {
            var applicationContext = context.ServiceProvider.GetRequiredService<ApplicationContext>();
            return applicationContext;
        }
    }
}
