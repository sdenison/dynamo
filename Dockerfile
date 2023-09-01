FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /source
COPY ./Source /source
RUN dotnet publish -c release -o web-build Dynamo.Web/Dynamo.Web.csproj
ENTRYPOINT [ "/bin/bash", "-l", "-c" ]

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as dynamo-web-server
WORKDIR /app
COPY --from=build /source/web-build .
#Answers on port 80 with any host name
ENTRYPOINT ["dotnet", "Dynamo.Web.dll" ]