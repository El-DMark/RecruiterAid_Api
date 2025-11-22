# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY RecruiterAid_Api/*.csproj ./RecruiterAid_Api/
RUN dotnet restore RecruiterAid_Api/RecruiterAid_Api.csproj

# Copy everything else and publish
COPY . .
RUN dotnet publish RecruiterAid_Api/RecruiterAid_Api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RecruiterAid_Api.dll"]
