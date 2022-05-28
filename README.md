# dynamo 

## Useful Commands

## Build web project
dotnet publish -c release -o dynamo-release Dynamo.Web/Dynamo.Web.csproj

## Run web server from command line
dotnet Dynamo.Web.dll --urls "http://localhost:5002"

## Run web server in Docker
sudo docker run -p 5002:80 dynamo-web-server

## Create stack
aws cloudformation create-stack --stack-name dynamo --template-body file://Root/root.yaml --parameters ParameterKey=EnvironmentName,ParameterValue=test ParameterKey=CFTemplateBucket,ParameterValue="sdenison-codebuild.s3.us-east-2.amazonaws.com" --capabilities CAPABILITY_NAMED_IAM

## Delete stack
aws cloudformation delete-stack --stack-name dynamo

## Delete stack
aws cloudformation update-stack --stack-name dynamo