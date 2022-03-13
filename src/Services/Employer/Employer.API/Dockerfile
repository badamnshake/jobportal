#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Employer/Employer.API/Employer.API.csproj", "Services/Employer/Employer.API/"]
COPY ["Services/Employer/Employer.DataAccess/Employer.DataAccess.csproj", "Services/Employer/Employer.DataAccess/"]
COPY ["Services/Employer/Employer.Infrastructure/Employer.Infrastructure.csproj", "Services/Employer/Employer.Infrastructure/"]
COPY ["Services/JobSeeker/JobSeeker.Infrastructure/JobSeeker.Infrastructure.csproj", "Services/JobSeeker/JobSeeker.Infrastructure/"]
COPY ["Services/Employer/Employer.BusinessLogic/Employer.BusinessLogic.csproj", "Services/Employer/Employer.BusinessLogic/"]
RUN dotnet restore "Services/Employer/Employer.API/Employer.API.csproj"
COPY . .
WORKDIR "/src/Services/Employer/Employer.API"
RUN dotnet build "Employer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Employer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Employer.API.dll"]
