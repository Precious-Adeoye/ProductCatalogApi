# ============================
# Build stage
# ============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ProductCatalogApi.sln .
COPY ProductCatalogApi/ProductCatalogApi.csproj ProductCatalogApi/

# Restore dependencies
RUN dotnet restore ProductCatalogApi/ProductCatalogApi.csproj

# Copy the rest of the source code
COPY . .

# Build and publish
WORKDIR /src/ProductCatalogApi
RUN dotnet publish ProductCatalogApi.csproj -c Release -o /app/publish

# ============================
# Runtime stage
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Expose API port
EXPOSE 8080

# Run the API
ENTRYPOINT ["dotnet", "ProductCatalogApi.dll"]
