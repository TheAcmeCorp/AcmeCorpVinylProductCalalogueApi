#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AcmeCorpVinylProductCalalogueApi/AcmeCorpVinylProductCalalogueApi.csproj", "AcmeCorpVinylProductCalalogueApi/"]
RUN dotnet restore "AcmeCorpVinylProductCalalogueApi/AcmeCorpVinylProductCalalogueApi.csproj"
COPY . .
WORKDIR "/src/AcmeCorpVinylProductCalalogueApi"
RUN dotnet build "AcmeCorpVinylProductCalalogueApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AcmeCorpVinylProductCalalogueApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AcmeCorpVinylProductCalalogueApi.dll"]