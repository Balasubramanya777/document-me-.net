FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY DocumentMe.API.sln .

COPY DocumentMe/DocumentMe.csproj DocumentMe/
COPY DocumentMe.DataAccessLayer/DocumentMe.DataAccessLayer.csproj DocumentMe.DataAccessLayer/
COPY DocumentMe.Repository/DocumentMe.Repository.csproj DocumentMe.Repository/
COPY DocumentMe.Service/DocumentMe.Service.csproj DocumentMe.Service/
COPY DocumentMe.Utility/DocumentMe.Utility.csproj DocumentMe.Utility/

RUN dotnet restore DocumentMe.API.sln

COPY . .

RUN dotnet publish DocumentMe/DocumentMe.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:${PORT}
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /publish .
CMD ["dotnet", "DocumentMe.dll"]
