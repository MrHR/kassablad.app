FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["kassablad.app.Client.csproj", "."]
RUN dotnet restore "kassablad.app.Client.csproj"
COPY . .
RUN dotnet build "kassablad.app.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "kassablad.app.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "kassablad.app.Client.dll"]