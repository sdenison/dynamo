# dynamo

Useful Commands:
#Build web project
dotnet publish -c release -o dynamo-release Dynamo.Web/Dynamo.Web.csproj
#Run web server
dotnet Dynamo.Web.dll --urls "http://localhost:5002"