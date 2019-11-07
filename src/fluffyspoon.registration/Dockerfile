FROM mcr.microsoft.com/dotnet/core/aspnet:3.0.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100 AS publish
WORKDIR /sln
COPY NuGet.Config .
COPY packages/ ./packages
COPY ["src/fluffyspoon.registration/fluffyspoon.registration.csproj", "src/fluffyspoon.registration/"]
RUN dotnet restore "src/fluffyspoon.registration/fluffyspoon.registration.csproj"
COPY . .
WORKDIR "src/fluffyspoon.registration"
<<<<<<< HEAD
ARG VERSION=1
RUN dotnet publish "fluffyspoon.registration.csproj" /p:Version=$VERSION -c Release -o /app/publish
=======
RUN dotnet build "fluffyspoon.registration.csproj" /p:Version=$VERSION -c Release  -o /app/build

FROM build AS publish
RUN dotnet publish "fluffyspoon.registration.csproj" -c Release -o /app/publish
>>>>>>> 2db433744aa6bf0a4872f5e82e6fbe57816cb805

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fluffyspoon.registration.dll"]