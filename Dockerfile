FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY src/DocumentMe.API/*.csproj src/DocumentMe.API/
RUN dotnet restore

COPY . .
WORKDIR /src/src/DocumentMe.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:${PORT}
COPY --from=build /app/publish .
CMD ["dotnet", "DocumentMe.API.dll"]
