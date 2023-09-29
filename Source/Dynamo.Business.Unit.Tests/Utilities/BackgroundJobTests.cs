using System;
using System.Collections.Generic;
using System.Linq;
using Csla;
using Csla.Configuration;
using Dynamo.Business.Utilities;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;
using Dynamo.Business.Shared.Utilities;
using Moq;

namespace Dynamo.Business.Unit.Tests.Utilities
{
    [TestFixture]
    public class BackgroundJobTests
    {
        IDataPortal<BackgroundJob> _portal;

        private static readonly List<BackgroundJobEntity> _backgroundJobTable = new List<BackgroundJobEntity>
        {
            new BackgroundJobEntity
            {
                Id = Guid.Parse("953526AB-899A-4F8C-A69C-DDB18E02BB49"), JobStatus = JobStatus.Running,
                JobType = JobType.BusyBox
            },
            new BackgroundJobEntity
            {
                Id = Guid.Parse("4C492CD6-BC51-4CA9-9FFF-52C2DE79329B"), JobStatus = JobStatus.FinishedSuccess,
                JobType = JobType.BusyBox
            }
        };

        [SetUp]
        public void SetUp()
        {
            var mockDataService = new Mock<IBackgroundJobDataService>();
            mockDataService.Setup(x => x.Insert(It.IsAny<BackgroundJobEntity>())).Returns(
                (BackgroundJobEntity entity) =>
                {
                    _backgroundJobTable.Add(entity);
                    return entity;
                });
            mockDataService.Setup(x => x.Update(It.IsAny<BackgroundJobEntity>())).Returns(
                (BackgroundJobEntity entity) =>
                {
                    var entityToUpdate = _backgroundJobTable.Single(x => x.Id.Equals(entity.Id));
                    entityToUpdate.JobStatus = entity.JobStatus;
                    entityToUpdate.JobType = entity.JobType;
                    return entityToUpdate;
                });
            mockDataService.Setup(x => x.Delete(It.IsAny<Guid>())).Returns(
                (Guid id) =>
                {
                    var toDelete = _backgroundJobTable.Single(x => x.Id.Equals(id));
                    _backgroundJobTable.Remove(toDelete);
                    return true;
                });
            mockDataService.Setup(x => x.Get(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                return _backgroundJobTable.Single(x => x.Id.Equals(id));
            });
            var services = new ServiceCollection();
            services.AddCsla();
            services.AddTransient<IBackgroundJobDataService>(o => mockDataService.Object);
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
            var jobAfterInsert = await job.SaveAsync();
            Assert.AreEqual(3, _backgroundJobTable.Count);
            job.JobStatus = JobStatus.FinishedSuccess;
            var jobAfterUpdate = await job.SaveAsync();
            job.Delete();
            var jobAfterDelete = await job.SaveAsync();
            //Why is this four.
            Assert.AreEqual(4, _backgroundJobTable.Count);
        }

        [Test]
        public async Task Can_fetch_BackgroundJob()
        {
            var job = await _portal.FetchAsync(Guid.Parse("953526AB-899A-4F8C-A69C-DDB18E02BB49"));
        }
    }
}
