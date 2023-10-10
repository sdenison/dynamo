using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3.Model;
using Dynamo.Config;
using System.Text.RegularExpressions;
using Dynamo.Commands.Utilities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

//Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Dynamo.Lambda
{
    public class Function
    {
        public async Task<string> FunctionHandler(string input, ILambdaContext context)
        {
            var functionOutput = await FunctionInvoker(input, context);
            return functionOutput;
        }

        public async Task<string> FunctionInvoker(string input, ILambdaContext context)
        {
            var newOut = new StringWriter();
            Console.SetOut(newOut);
            var args = Regex.Matches(input, @"[\""].+?[\""]|[^ ]+")
                .Select(m => m.Value.Replace("\"", ""))
                .ToArray();
            await Program.InvokeAsync(args);
            return newOut.ToString();
        }

        public async Task<string> BackgroundJobHandler(S3Event evnt, ILambdaContext context)
        {
            var backgroundJobCommand = new RunBackgroundJobCommand();
            var jobId = evnt.Records[0].S3.Object.Key;
            var bucketName = evnt.Records[0].S3.Bucket.Name;
            backgroundJobCommand.RunBackgroundJob(jobId);
            return "BackgroundJobHandler done";
        }
    }
}
