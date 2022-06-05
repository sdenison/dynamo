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

## Delete stackaws ecr get-login-password --region us-east-2 | sudo docker login --username AWS --password-stdin 477374169746.dkr.ecr.us-east-2.amazonaws.com
IMAGES_TO_DELETE=$( aws ecr list-images --region $ECR_REGION --repository-name $ECR_REPO --filter "tagStatus=UNTAGGED" --query 'imageIds[*]' --output json )
ecr list-images --region $ECR_REGION --repository-name $ECR_REPO --filter "tagStatus=UNTAGGED" imageIds[*]' --output json )
aws ecr batch-delete-image --repository-name test-dynamo-repository --image-ids imageTag=latest |
aws cloudformation delete-stack --stack-name dynamo

## Login to remote Docker


## Push Docker image
sudo docker push 477374169746.dkr.ecr.us-east-2.amazonaws.com/test-dynamo-repository

## Running the app
When the app is running you can navigate to it by using the public dns of the load balancer.

