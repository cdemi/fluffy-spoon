FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.2 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.200 AS publish
WORKDIR /sln
COPY NuGet.Config .
COPY push.sh .
COPY packages/ ./packages
COPY ["src/DemoFluffySpoon.Registration/DemoFluffySpoon.Registration.csproj", "src/DemoFluffySpoon.Registration/"]
RUN dotnet restore "src/DemoFluffySpoon.Registration/DemoFluffySpoon.Registration.csproj"
COPY . .
WORKDIR "src/DemoFluffySpoon.Registration"
ARG VERSION=1
RUN dotnet publish "DemoFluffySpoon.Registration.csproj" /p:Version=$VERSION -c Release -o /app/publish
ARG GH_TOKEN=
RUN /sln/push.sh

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoFluffySpoon.Registration.dll"]
