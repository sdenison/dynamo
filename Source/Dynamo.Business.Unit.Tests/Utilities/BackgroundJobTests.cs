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
        IDataPortal<BackgroundJobList> _listPortal;

        private static readonly List<BackgroundJobEntity> _backgroundJobTable = new List<BackgroundJobEntity>
        {
            new BackgroundJobEntity
            {
                Id = Guid.Parse("953526AB-899A-4F8C-A69C-DDB18E02BB49"), JobStatus = JobStatus.Running,
                JobType = JobType.BusyBox,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
            },
            new BackgroundJobEntity
            {
                Id = Guid.Parse("4C492CD6-BC51-4CA9-9FFF-52C2DE79329B"), JobStatus = JobStatus.FinishedSuccess,
                JobType = JobType.BusyBox,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now
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
            mockDataService.Setup(x => x.GetAll()).Returns(() =>
                {
                    return _backgroundJobTable;
                }
            );
            var services = new ServiceCollection();
            services.AddCsla(o => o
                .DataPortal(dpo => dpo
                    .ClientSideDataPortal(x => x.UseLocalProxy())));
            services.AddTransient<IBackgroundJobDataService>(o => mockDataService.Object);
            var serviceProvider = services.BuildServiceProvider();
            _portal = serviceProvider.GetRequiredService<IDataPortal<BackgroundJob>>();
            _listPortal = serviceProvider.GetRequiredService<IDataPortal<BackgroundJobList>>();
        }

        [Test]
        public async Task Can_create_BackgroundJob()
        {
            BackgroundJob job = await _portal.CreateAsync();
            Assert.That(job, Is.Not.Null);
            Assert.That(job.IsNew);
            Assert.That(job.IsDirty);
            Assert.That(JobStatus.Initializing, Is.EqualTo(job.JobStatus));
            job.JobStatus = JobStatus.Submitted;
            job = await job.SaveAsync();
            Assert.That(3, Is.EqualTo(_backgroundJobTable.Count));
            job.JobStatus = JobStatus.FinishedSuccess;
            Assert.That(job.IsDirty);
            var jobAfterUpdate = await job.SaveAsync();
            job.Delete();
            Assert.That(3, Is.EqualTo(_backgroundJobTable.Count));
            var jobAfterDelete = await job.SaveAsync();
            Assert.That(2, Is.EqualTo(_backgroundJobTable.Count));
        }

        [Test]
        public async Task Can_fetch_BackgroundJob()
        {
            var job = await _portal.FetchAsync(Guid.Parse("953526AB-899A-4F8C-A69C-DDB18E02BB49"));
        }

        [Test]
        public async Task Can_get_list_of_BackgroundJobs()
        {
            BackgroundJobList jobs = await _listPortal.FetchAsync();
            Assert.That(jobs, Is.Not.Null);
            Assert.That(jobs.Count > 0);
        }
    }
}
