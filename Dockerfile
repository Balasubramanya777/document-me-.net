# =============================
# Build stage
# =============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY NuGet.Docker.Config ./NuGet.Config

# Copy solution and project files
COPY DocumentMe.API.sln .
COPY DocumentMe/DocumentMe.API.csproj DocumentMe/
COPY DocumentMe.Service/DocumentMe.Service.csproj DocumentMe.Service/
COPY DocumentMe.Repository/DocumentMe.Repository.csproj DocumentMe.Repository/
COPY DocumentMe.DataAccessLayer/DocumentMe.DataAccessLayer.csproj DocumentMe.DataAccessLayer/
COPY DocumentMe.Utility/DocumentMe.Utility.csproj DocumentMe.Utility/

# Restore dependencies
RUN dotnet restore DocumentMe/DocumentMe.API.csproj \
    --configfile NuGet.Config

# Copy everything else
COPY . .

# Publish
RUN dotnet publish DocumentMe/DocumentMe.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    --configfile NuGet.Config

# =============================
# Runtime stage
# =============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "DocumentMe.API.dll"]
