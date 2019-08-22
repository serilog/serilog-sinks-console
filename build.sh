#!/bin/bash

set -e
dotnet --info
dotnet restore

for path in src/**/*.csproj; do
    dotnet build -f netstandard1.3 -c Release ${path}
    dotnet build -f netstandard2.0 -c Release ${path}
    dotnet build -f netcoreapp1.1 -c Release ${path}
done

for path in test/*.Tests/*.csproj; do
    dotnet test -f netcoreapp1.1 -c Release ${path}
    dotnet test -f netcoreapp2.2 -c Release ${path}
done

for path in sample/ConsoleDemo/*.csproj; do
    dotnet build -f netcoreapp1.1 -c Release ${path}
    dotnet run -f netcoreapp1.1 --project ${path}
    dotnet build -f netcoreapp2.2 -c Release ${path}
    dotnet run -f netcoreapp2.2 --project ${path}
done
