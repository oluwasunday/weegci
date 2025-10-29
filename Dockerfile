# Use official .NET 8 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and build
COPY . .
RUN dotnet publish -c Release -o out

# Use smaller ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose port 8080 (Render default)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Run the app
ENTRYPOINT ["dotnet", "weegci.dll"]
