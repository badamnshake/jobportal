#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/JobSeeker/JobSeeker.API/JobSeeker.API.csproj", "Services/JobSeeker/JobSeeker.API/"]
COPY ["Services/JobSeeker/JobSeeker.DataAccess/JobSeeker.DataAccess.csproj", "Services/JobSeeker/JobSeeker.DataAccess/"]
COPY ["Services/JobSeeker/JobSeeker.Infrastructure/JobSeeker.Infrastructure.csproj", "Services/JobSeeker/JobSeeker.Infrastructure/"]
COPY ["Services/JobSeeker/JobSeeker.BusinessLogic/JobSeeker.BusinessLogic.csproj", "Services/JobSeeker/JobSeeker.BusinessLogic/"]
RUN dotnet restore "Services/JobSeeker/JobSeeker.API/JobSeeker.API.csproj"
COPY . .
WORKDIR "/src/Services/JobSeeker/JobSeeker.API"
RUN dotnet build "JobSeeker.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobSeeker.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobSeeker.API.dll"]
