FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY ./Source /source
RUN apt-get update && apt-get install -y clang zlib1g-dev \
    && rm -rf /var/lib/apt/lists/*
RUN dotnet publish -c Release -o /web-build Dynamo.Ui.Blazor.Server/Dynamo.Ui.Blazor.Server.csproj

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS dynamo-web-server
WORKDIR /app
COPY --from=build /web-build .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Dynamo.Ui.Blazor.Server.dll"]