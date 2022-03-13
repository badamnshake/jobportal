#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WebApp/ApiGateways/VacancyRequests.Aggregator/VacancyRequests.Aggregator.csproj", "WebApp/ApiGateways/VacancyRequests.Aggregator/"]
RUN dotnet restore "WebApp/ApiGateways/VacancyRequests.Aggregator/VacancyRequests.Aggregator.csproj"
COPY . .
WORKDIR "/src/WebApp/ApiGateways/VacancyRequests.Aggregator"
RUN dotnet build "VacancyRequests.Aggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VacancyRequests.Aggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VacancyRequests.Aggregator.dll"]
