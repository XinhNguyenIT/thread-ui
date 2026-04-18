#!/bin/bash

set -e

cd Packages/Backend || exit

ENV=$1

if [ -z "$ENV" ]; then
  echo "❌ Missing environment (dev/stag)"
  exit 1
fi

echo "🚀 Starting seed..."
echo "📦 Environment: $ENV"

dotnet run seed $ENV