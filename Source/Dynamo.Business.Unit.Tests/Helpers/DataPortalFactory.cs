using Csla;
using Microsoft.Extensions.DependencyInjection;

namespace Dynamo.Business.Unit.Tests.Helpers
{
    public static class DataPortalFactory
    {
        public static IDataPortal<T> CreateDataPortal<T>(TestDIContext context)
        {
            var dataPortal = context.ServiceProvider.GetRequiredService<IDataPortal<T>>();
            return dataPortal;
        }

        public static IChildDataPortal<T> CreateChildDataPortal<T>(TestDIContext context)
        {
            var dataPortal = context.ServiceProvider.GetRequiredService<IChildDataPortal<T>>();
            return dataPortal;
        }
    }
}
