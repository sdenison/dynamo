# dynamo 

## Useful Commands

## Build web project

dotnet publish -c release -o dynamo-release Dynamo.Web/Dynamo.Web.csproj

## Run web server from command line

dotnet Dynamo.Web.dll --urls "http://localhost:5002"

## Run web server in Docker
sudo docker run -p 5002:80 dynamo-web-server