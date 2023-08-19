sed -i "s/@BUILD_NUMBER@/$CODEBUILD_BUILD_NUMBER/" Source/Dynamo/appsettings.json 

dotnet publish -c Release -o dynamo-lambda-release Source/Dynamo.Lambda/Dynamo.Lambda.csproj
cd dynamo-lambda-release
zip -r $ENVIRONMENT_NAME-dynamo-dontnet-lambda-functions.zip *
cp $ENVIRONMENT_NAME-dynamo-dontnet-lambda-functions.zip $ENVIRONMENT_NAME-dynamo-dontnet-lambda-functions-v$CODEBUILD_BUILD_NUMBER.zip
aws s3 cp $ENVIRONMENT_NAME-dynamo-dontnet-lambda-functions-v$CODEBUILD_BUILD_NUMBER.zip s3://$ENVIRONMENT_NAME-dynamo-deployment/
aws lambda update-function-code --function-name $ENVIRONMENT_NAME-dynamo-dotnet-lambda --s3-bucket $ENVIRONMENT_NAME-dynamo-deployment --s3-key $ENVIRONMENT_NAME-dynamo-dontnet-lambda-functions-v$CODEBUILD_BUILD_NUMBER.zip