FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
# <add your commands here>

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API-Gateway/API-Gateway.csproj", "API-Gateway/"]
RUN dotnet restore "API-Gateway/API-Gateway.csproj"
COPY . .
WORKDIR "/src/API-Gateway"
RUN dotnet build "API-Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API-Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API-Gateway.dll"]