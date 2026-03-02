# Create the Dockerfile directly from terminal:

# On Linux/Mac:
cat > Dockerfile << 'EOF'
# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PMSYSAPI.csproj", "."]
RUN dotnet restore

COPY . .
RUN dotnet publish "PMSYSAPI.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PMSYSAPI.dll"]
EOF

# On Windows PowerShell:
@"
# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PMSYSAPI.csproj", "."]
RUN dotnet restore

COPY . .
RUN dotnet publish "PMSYSAPI.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PMSYSAPI.dll"]
"@ | Out-File -FilePath Dockerfile -Encoding utf8