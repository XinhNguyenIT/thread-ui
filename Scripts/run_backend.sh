#!/bin/bash

set -e

cd Packages/Backend || exit

echo "🔨 Building..."
dotnet build

echo "🚀 Running..."
dotnet run