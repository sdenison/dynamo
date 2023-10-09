FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /source
COPY ./Source /source
RUN apt update
RUN apt install -y clang zlib1g-dev
#RUN dotnet publish -c release -o web-build Dynamo.Web/Dynamo.Web.csproj
RUN dotnet publish -c release -o web-build Dynamo.Ui.Blazor.Server 
RUN dotnet publish -c release -o lambda-build /p:GenerateRuntimeConfigurationFiles=true --runtime linux-x64 Dynamo.Lambda/Dynamo.Lambda.csproj 
ENTRYPOINT [ "/bin/bash", "-l", "-c" ]

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as dynamo-web-server
WORKDIR /app
COPY --from=build /source/web-build .
EXPOSE 80
EXPOSE 443 
#Answers on port 80 with any host name 
#ENTRYPOINT [ "pwsh" ]
#ENTRYPOINT [ "/bin/bash" ]
#ENTRYPOINT [ "/bin/bash", "-l", "-c" ]
ENTRYPOINT ["dotnet", "Dynamo.Ui.Blazor.Server.dll" ]

FROM public.ecr.aws/lambda/dotnet:7 as lambda
COPY --from=build /source/lambda-build ${LAMBDA_TASK_ROOT}
CMD [ "Dynamo.Lambda::Dynamo.Lambda.Function::FunctionHandler" ]