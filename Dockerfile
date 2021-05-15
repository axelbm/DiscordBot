#FROM mcr.microsoft.com/dotnet/aspnet:5.0
#COPY bin/Release/net5.0/publish/ App/
#WORKDIR /App
#ENTRYPOINT ["dotnet", "DiscordBot.dll"]

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# copy and publish app and libraries
COPY . .
RUN dotnet publish -c release -o /App

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /App
COPY --from=build /App .
#ENTRYPOINT ["dotnet", "dotnetapp.dll"]
ENTRYPOINT ["dotnet", "DiscordBot.dll"]
