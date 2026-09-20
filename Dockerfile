# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["TrackFlow.sln", "."]
COPY ["src/TrackFlow.Domain/TrackFlow.Domain.csproj", "src/TrackFlow.Domain/"]
COPY ["src/TrackFlow.Application/TrackFlow.Application.csproj", "src/TrackFlow.Application/"]
COPY ["src/TrackFlow.Infrastructure/TrackFlow.Infrastructure.csproj", "src/TrackFlow.Infrastructure/"]
COPY ["src/TrackFlow.WebAPI/TrackFlow.WebAPI.csproj", "src/TrackFlow.WebAPI/"]

# Restore dependencies
RUN dotnet restore "TrackFlow.sln"

# Copy all source code
COPY . .

# Build the application
RUN dotnet build "src/TrackFlow.WebAPI/TrackFlow.WebAPI.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "src/TrackFlow.WebAPI/TrackFlow.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "TrackFlow.WebAPI.dll"]
