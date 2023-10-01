using Amazon;
using Amazon.DynamoDBv2;
using Csla;
using Csla.Configuration;
using Dynamo.Business.Shared.Utilities;
using Dynamo.Business.Utilities;
using Dynamo.Data.DynamoDb.Utilities;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ServiceStack.Aws.DynamoDb;

namespace Dynamo.Data.DynamoDb.Integration.Utilities
{
    [TestFixture]
    public class BackgroundJobDataServiceTests
    {
        [Test, Ignore("This integration test can be used to debug DynamoDb issues")]
        //[Test]
        public void Can_read_and_write_to_dynamodb()
        {
            var awsDb = new AmazonDynamoDBClient(RegionEndpoint.USEast2);
            var db = new PocoDynamo(awsDb);
            db.RegisterTable<BackgroundJobEntity>();
            var dataService = new BackgroundJobDataService(db);
            var backgroundJob = new BackgroundJobEntity()
            {
                Id = Guid.NewGuid(),
                JobStatus = JobStatus.Initializing,
                JobType = JobType.BusyBox,
            };
            dataService.Insert(backgroundJob);
            var insertedEntity = dataService.Get(backgroundJob.Id);
            backgroundJob.JobStatus = JobStatus.Submitted;
            dataService.Update(backgroundJob);
            dataService.Delete(backgroundJob.Id);
        }

        [Test]
        public void Use_csla_BackgroundJob_and_dynamo_together()
        {
            var awsDb = new AmazonDynamoDBClient(RegionEndpoint.USEast2);
            var db = new PocoDynamo(awsDb);
            db.RegisterTable<BackgroundJobEntity>();
            var dataService = new BackgroundJobDataService(db);

            var services = new ServiceCollection();
            services.AddCsla();
            services.AddTransient<IBackgroundJobDataService>(o => dataService);
            var serviceProvider = services.BuildServiceProvider();
            var portal = serviceProvider.GetRequiredService<IDataPortal<BackgroundJob>>();

            var job = portal.Create();
            Assert.IsTrue(job.IsNew);
            job = job.Save();
        }
    }
}
