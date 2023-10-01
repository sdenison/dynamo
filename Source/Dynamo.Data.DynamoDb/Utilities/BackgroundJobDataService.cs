using Dynamo.Business.Shared.Utilities;
using ServiceStack;
using ServiceStack.Aws.DynamoDb;

namespace Dynamo.Data.Dynamo.Utilities
{
    public class BackgroundJobDataService : Service, IBackgroundJobDataService
    {
        public IPocoDynamo Dynamo { get; set; }

        public BackgroundJobDataService(IPocoDynamo dynamo)
        {
            Dynamo = dynamo;
        }

        public BackgroundJobEntity Get(Guid id)
        {
            return Dynamo.GetItem<BackgroundJobEntity>(id);
        }

        public BackgroundJobEntity Insert(BackgroundJobEntity entity)
        {
            Dynamo.PutItem(entity);
            return entity;
        }

        public BackgroundJobEntity Update(BackgroundJobEntity entity)
        {
            Dynamo.PutItem(entity);
            return entity;
        }

        public bool Delete(Guid id)
        {
            Dynamo.DeleteItem<BackgroundJobEntity>(id.ToString());
            return true;
        }
    }
}
