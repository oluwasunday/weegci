# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files first for dependency restore
COPY weegci.models/weegci.models.csproj weegci.models/
COPY weegci.services/weegci.services.csproj weegci.services/
COPY weegci/weegci.csproj weegci/

# Restore dependencies for the main app (this will also pull class libraries)
RUN dotnet restore weegci/weegci.csproj

# Copy the entire solution
COPY . .

# Build the main project in Release mode
WORKDIR /src/weegci
RUN dotnet build weegci.csproj -c Release -o /app/build

# Publish the app
RUN dotnet publish weegci.csproj -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET Core runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "weegci.dll"]
