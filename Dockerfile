FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /source
COPY ./Source /source
RUN apt update
RUN apt install -y clang zlib1g-dev
RUN dotnet publish -c release -o web-build Dynamo.Ui.Blazor.Server/Dynamo.Ui.Blazor.Server.csproj 
ENTRYPOINT [ "/bin/bash", "-l", "-c" ]

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as dynamo-web-server
WORKDIR /app
COPY --from=build /source/web-build .
EXPOSE 80
EXPOSE 443 
#Answers on port 80 with any host name 
ENTRYPOINT ["dotnet", "Dynamo.Ui.Blazor.Server.dll" ]