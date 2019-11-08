#/bin/sh
if [ -z "$GH_TOKEN" ]
then
      echo "Skipping NuGet Push..."
else
      dotnet nuget push /sln/artifacts/**.nupkg -s "https://nuget.pkg.github.com/cdemi/index.json" -k $GH_TOKEN
fi