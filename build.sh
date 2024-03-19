#!/bin/bash

set -e

echo "🤖 Attempting to build..."

dotnet build --configuration Release

echo "🤖 Running tests..."
dotnet test test/Serilog.Sinks.Console.Tests --configuration Release --no-build --no-restore
