#!/bin/bash

# Navigate to the Data Access or Web API layer where your DbContext is located
# cd Backend/YourProject.Api

cd Packages/Backend || exit

echo "------------------------------------------"
echo "🚀 Starting Database Migration..."
echo "------------------------------------------"

# Execute the update command
dotnet ef database update

# Check if the command succeeded
if [ $? -eq 0 ]; then
    echo "------------------------------------------"
    echo "✅ Success: Database is up to date!"
    echo "------------------------------------------"
else
    echo "------------------------------------------"
    echo "❌ Error: Migration failed. Check your connection string or Build errors."
    echo "------------------------------------------"
    exit 1
fi