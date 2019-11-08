#/bin/sh
if [ -z "$1" ]
then
      echo "Skipping NuGet Push..."
else
      dotnet nuget push /sln/**/*.nupkg -s "https://nuget.pkg.github.com/cdemi/index.json" -k $1:$2
fi