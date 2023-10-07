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
    //[TestFixture, Ignore("Uses live connection to DynamoDB")]
    [TestFixture]
    public class BackgroundJobDataServiceTests
    {
        [Test]
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
            //Set up DynamoDb dataService
            var awsDb = new AmazonDynamoDBClient(RegionEndpoint.USEast2);
            var db = new PocoDynamo(awsDb);
            var metadata = db.RegisterTable<BackgroundJobEntity>();
            var m = metadata.GetTableMetadata(typeof(BackgroundJobEntity));
            m.Name = "Test-BackgroundJobEntity";

            var dataService = new BackgroundJobDataService(db);

            //Add dependency injection 
            var services = new ServiceCollection();
            services.AddCsla();
            services.AddTransient<IBackgroundJobDataService>(o => dataService);
            var serviceProvider = services.BuildServiceProvider();
            var portal = serviceProvider.GetRequiredService<IDataPortal<BackgroundJob>>();

            //Create a new backround service job
            var job = portal.Create();
            Assert.IsTrue(job.IsNew);
            job = job.Save();
            //Make a change
            job.JobStatus = JobStatus.FinishedSuccess;
            job.JobOutput = "The answer to your question is 42";
            //Pull from the database before changes is saved
            var jobFromDatabase = portal.Fetch(job.Id);
            Assert.AreEqual(JobStatus.Initializing, jobFromDatabase.JobStatus);
            //Actually save 
            job = job.Save();
            //Delete the job
            //job.Delete();
            job = job.Save();
        }
    }
}
