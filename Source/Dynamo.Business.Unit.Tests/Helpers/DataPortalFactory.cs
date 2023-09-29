using Csla;
using Microsoft.Extensions.DependencyInjection;

namespace Dynamo.Business.Unit.Tests.Helpers
{
    public static class DataPortalFactory
    {
        public static IDataPortal<T> CreateDataPortal<T>(TestDIContext context)
        {
            IDataPortal<T> dataPortal;

            dataPortal = context.ServiceProvider.GetRequiredService<IDataPortal<T>>();
            return dataPortal;
        }

        public static IChildDataPortal<T> CreateChildDataPortal<T>(TestDIContext context)
        {
            IChildDataPortal<T> dataPortal;

            dataPortal = context.ServiceProvider.GetRequiredService<IChildDataPortal<T>>();
            return dataPortal;
        }
    }
}
