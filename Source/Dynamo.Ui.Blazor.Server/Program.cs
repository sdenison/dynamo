using Amazon;
using Amazon.DynamoDBv2;
using Csla.Configuration;
using Dynamo.Business.Shared.Utilities;
using Dynamo.Data.DynamoDb.Utilities;
using Dynamo.IO.S3.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ServiceStack.Aws.DynamoDb;

string BlazorClientPolicy = "AllowAllOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(BlazorClientPolicy,
      builder =>
      {
          builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
      });
});

builder.Services.AddControllers();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCsla(o => o
  .AddAspNetCore()
  .AddServerSideBlazor(o => o.UseInMemoryApplicationContextManager = false)
  .Security(so => so.FlowSecurityPrincipalFromClient = false)
  .DataPortal(dpo => dpo
    .AddServerSideDataPortal()
    .ClientSideDataPortal(co => co
        .UseLocalProxy())));

//Set up DynamoDb dataService
var awsDb = new AmazonDynamoDBClient(RegionEndpoint.USEast2);
var db = new PocoDynamo(awsDb);
db.RegisterTable<BackgroundJobEntity>();
var metadata = db.GetTableMetadata(typeof(BackgroundJobEntity));
metadata.Name = "test-BackgroundJob";
var dataService = new BackgroundJobDataService(db);

//Add dependency injection 
builder.Services.AddTransient<IBackgroundJobDataService>(o => dataService);
var storageService = new StorageService();
builder.Services.AddTransient<IStorageService>(o => storageService);

builder.Services.AddBlazorBootstrap(); // Add this line

// If using Kestrel:
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

// If using IIS:
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
