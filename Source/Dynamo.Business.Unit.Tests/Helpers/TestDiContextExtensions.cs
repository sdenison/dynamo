using Csla;

namespace Dynamo.Business.Unit.Tests.Helpers
{
    public static class TestDiContextExtensions
    {
        public static ApplicationContext CreateTestApplicationContext(this TestDIContext context)
        {
            return ApplicationContextFactory.CreateTestApplicationContext(context);
        }

        public static IDataPortal<T> CreateDataPortal<T>(this TestDIContext context)
        {
            return DataPortalFactory.CreateDataPortal<T>(context);
        }

        public static IChildDataPortal<T> CreateChildDataPortal<T>(this TestDIContext context)
        {
            return DataPortalFactory.CreateChildDataPortal<T>(context);
        }
    }
}
