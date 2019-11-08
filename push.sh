#/bin/sh
if [ -z "$GH_USERNAME" ]
then
      echo "Skipping NuGet Push..."
else
      dotnet nuget push /sln/**/*.nupkg -s "https://nuget.pkg.github.com/cdemi/index.json" -k $GH_USERNAME:$GH_TOKEN
fi