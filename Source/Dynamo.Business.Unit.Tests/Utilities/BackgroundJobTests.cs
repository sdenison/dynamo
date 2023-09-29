using Csla;
using Csla.Configuration;
using Dynamo.Business.Utilities;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Dynamo.Business.Unit.Tests.Utilities
{
    [TestFixture]
    public class BackgroundJobTests
    {
        IDataPortal<BackgroundJob> _portal;

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();
            services.AddCsla();
            var serviceProvider = services.BuildServiceProvider();
            _portal = serviceProvider.GetRequiredService<IDataPortal<BackgroundJob>>();
        }

        [Test]
        public async Task Can_create_BackgroundJob()
        {
            BackgroundJob job = await _portal.CreateAsync();
            Assert.IsNotNull(job);
            Assert.IsTrue(job.IsNew);
            Assert.IsTrue(job.IsDirty);
            Assert.AreEqual(JobStatus.Initializing, job.JobStatus);
            job.JobStatus = JobStatus.Submitted;
        }
    }
}
