using Amazon;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Csla;
using Csla.Configuration;
using Dynamo.Business.Shared.Utilities;
using Dynamo.Business.Utilities;
using Dynamo.Data.DynamoDb.Utilities;
using ServiceStack.Aws.DynamoDb;

namespace Dynamo.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisteredServiceCollection { get; set; }

        public static IServiceProvider GetBaseServices()
        {
            if (RegisteredServiceCollection != null)
                return RegisteredServiceCollection.BuildServiceProvider();
            var serviceCollection = new ServiceCollection();

            //Set up DynamoDb dataService
            var awsDb = new AmazonDynamoDBClient(RegionEndpoint.USEast2);
            var db = new PocoDynamo(awsDb);
            db.RegisterTable<BackgroundJobEntity>();
            var metadata = db.GetTableMetadata(typeof(BackgroundJobEntity));
            metadata.Name = "test-BackgroundJob";
            var dataService = new BackgroundJobDataService(db);

            //Add dependency injection 
            serviceCollection.AddCsla();
            serviceCollection.AddTransient<IBackgroundJobDataService>(o => dataService);
            return serviceCollection.BuildServiceProvider();
        }
    }
}
