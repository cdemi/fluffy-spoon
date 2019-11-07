FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0 AS base
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100 AS build
COPY NuGet.Config .
COPY ["src/fluffyspoon.registration/fluffyspoon.registration.csproj", "src/fluffyspoon.registration/"]
RUN dotnet restore "src/fluffyspoon.registration/fluffyspoon.registration.csproj"
COPY . .
WORKDIR "src/fluffyspoon.registration"
RUN dotnet build "fluffyspoon.registration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "fluffyspoon.registration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fluffyspoon.registration.dll"]