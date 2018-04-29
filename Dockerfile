FROM microsoft/dotnet:2.1.0-preview2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1.300-preview2-sdk AS build
WORKDIR /src
COPY FinpeApi.sln ./
COPY FinpeApi/FinpeApi.csproj FinpeApi/
WORKDIR /src/FinpeApi
RUN dotnet restore -nowarn:msb3202,nu1503
WORKDIR /src
COPY . .
WORKDIR /src/FinpeApi
RUN dotnet build FinpeApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish FinpeApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FinpeApi.dll"]
