#/bin/sh
if [ -z "$GH_TOKEN" ]
then
      echo "Skipping NuGet Push..."
else
      dotnet nuget push /sln/artifacts/**.nupkg -s "https://api.nuget.org/v3/index.json" -k $GH_TOKEN -n true
fi