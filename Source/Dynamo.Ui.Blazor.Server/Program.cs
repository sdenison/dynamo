using Amazon;
using Amazon.DynamoDBv2;
using Csla;
using Csla.Configuration;
using Dynamo.Business.Shared.Utilities;
using Dynamo.Business.Utilities;
using Dynamo.Data.DynamoDb.Utilities;
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

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();
builder.Services.AddCsla(o => o
  .AddAspNetCore()
  .DataPortal(dpo => dpo
    .EnableSecurityPrincipalFlowFromClient()
    .AddServerSideDataPortal()
    .UseLocalProxy()));

//for EF Db
//builder.Services.AddTransient(typeof(DataAccess.IPersonDal), typeof(DataAccess.EF.PersonEFDal));
//builder.Services.AddDbContext<DataAccess.EF.PersonDbContext>(
//options => options.UseSqlServer("Server=servername;Database=personDB;User ID=sa; Password=pass;Trusted_Connection=True;MultipleActiveResultSets=true"));

// for Mock Db
//builder.Services.AddTransient(typeof(DataAccess.IPersonDal), typeof(DataAccess.Mock.PersonDal));

//Set up DynamoDb dataService
var awsDb = new AmazonDynamoDBClient(RegionEndpoint.USEast2);
var db = new PocoDynamo(awsDb);
db.RegisterTable<BackgroundJobEntity>();
var metadata = db.GetTableMetadata(typeof(BackgroundJobEntity));
metadata.Name = "test-BackgroundJob";
var dataService = new BackgroundJobDataService(db);

//Add dependency injection 
//var services = new ServiceCollection();
builder.Services.AddCsla();
builder.Services.AddTransient<IBackgroundJobDataService>(o => dataService);
//var serviceProvider = services.BuildServiceProvider();
//var portal = serviceProvider.GetRequiredService<IDataPortal<BackgroundJob>>();

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
