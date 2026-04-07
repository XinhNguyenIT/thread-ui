#!/bin/bash

set -e

cd Backend || exit

echo "🔨 Building..."
dotnet build

echo "🚀 Running..."
dotnet run