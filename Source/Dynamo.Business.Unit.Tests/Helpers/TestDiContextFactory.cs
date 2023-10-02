using System;
using System.Security.Claims;
using System.Security.Principal;
using Csla;
using Csla.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Dynamo.Business.Unit.Tests.Helpers
{
    public static class TestDiContextFactory
    {
        public static TestDIContext CreateDefaultContext()
        {
            // Create a default security principal
            var principal = CreateDefaultClaimsPrincipal();

            // Delegate to the other overload to create the context
            return CreateContext(principal);
        }

        public static TestDIContext CreateContext(ClaimsPrincipal principal)
        {
            return CreateContext(null, principal);
        }

        public static TestDIContext CreateContext(Action<CslaOptions> customCslaOptions)
        {
            var principal = CreateDefaultClaimsPrincipal();
            return CreateContext(customCslaOptions, principal);
        }

        public static TestDIContext CreateContext(Action<CslaOptions> customCslaOptions, ClaimsPrincipal principal)
        {
            // Initialise DI
            var services = new ServiceCollection();

            // Add Csla
            services.TryAddSingleton<Csla.Core.IContextManager, ApplicationContextManagerUnitTests>();
            services.TryAddSingleton<Csla.Server.Dashboard.IDashboard, Csla.Server.Dashboard.Dashboard>();
            services.AddCsla(customCslaOptions);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            // Initialise CSLA security
            var context = serviceProvider.GetRequiredService<ApplicationContext>();
            context.Principal = principal;

            return new TestDIContext(serviceProvider);
        }

        private static ClaimsPrincipal CreateDefaultClaimsPrincipal()
        {
            // Create a default security principal
            var identity = new ClaimsIdentity(new GenericIdentity("Fred"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Users"));
            return new ClaimsPrincipal(identity);
        }
    }
}
