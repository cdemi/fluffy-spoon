#/bin/sh
if [ -z "$GH_TOKEN" ]
then
      echo "Skipping NuGet Push..."
else
      dotnet nuget push /sln/artifacts/**.nupkg -k $GH_TOKEN
fi