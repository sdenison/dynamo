using Amazon;
using Amazon.DynamoDBv2;
using Dynamo.Business.Shared.Utilities;
using Dynamo.Business.Utilities;
using Dynamo.Data.Dynamo.Utilities;
using NUnit.Framework;
using ServiceStack.Aws.DynamoDb;

namespace Dynamo.Data.Dynamo.Integration.NewFolder
{
    [TestFixture]
    public class BackgroundJobDataServiceTests
    {
        [Test, Ignore("This integration test can be used to debug DynamoDb issues")]
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
    }
}
