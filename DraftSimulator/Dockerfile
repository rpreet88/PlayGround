# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./DraftSimulator.csproj" --disable-parallel
RUN dotnet publish "./DraftSimulator.csproj" -c release -o /publish --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY --from=build /publish ./

EXPOSE 5000
ENV DOTNET_HTTP_PORT=http://+:5000
ENV DOTNET_URLS=http://+:5000

ENTRYPOINT [ "dotnet", "DraftSimulator.dll" ]