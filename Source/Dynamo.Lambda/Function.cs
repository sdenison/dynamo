using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Text.RegularExpressions;

//Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Dynamo.Lambda
{
    public class Function
    {
        public APIGatewayHttpApiV2ProxyResponse FunctionHandler(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
        {        
            request.QueryStringParameters.TryGetValue("name", out var name);
            name = name ?? "John Doe";
            var message = $"Hello {name}, from AWS Lambda";
            return new APIGatewayHttpApiV2ProxyResponse
            {
                Body = message,
                StatusCode = 200
            };
        }
        //public async Task<string> FunctionHandler(string input, ILambdaContext context)
        //{
        //    var functionOutput = await FunctionInvoker(input, context);
        //    return functionOutput;
        //}

        //public async Task<string> FunctionInvoker(string input, ILambdaContext context)
        //{
        //    var newOut = new StringWriter();
        //    Console.SetOut(newOut);
        //    var args = Regex.Matches(input, @"[\""].+?[\""]|[^ ]+")
        //        .Select(m => m.Value.Replace("\"", ""))
        //        .ToArray();
        //    await Program.InvokeAsync(args);
        //    return newOut.ToString();
        //}
    }
}
