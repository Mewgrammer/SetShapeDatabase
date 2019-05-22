FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 30000
EXPOSE 44300

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["SetShapeDatabase/SetShapeDatabase.csproj", "SetShapeDatabase/"]
RUN dotnet restore "SetShapeDatabase/SetShapeDatabase.csproj"
COPY . .
WORKDIR "/src/SetShapeDatabase"
RUN dotnet build "SetShapeDatabase.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SetShapeDatabase.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SetShapeDatabase.dll"]