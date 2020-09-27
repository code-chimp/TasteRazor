#!/usr/bin/env bash

if [ -z "$1" ]; then
    echo "You need to supply a name for the migration"
    exit 1
fi

dotnet ef migrations add $1 -s ./TasteRazor/TasteRazor.csproj -p ./TasteRazor.DataAccess/TasteRazor.DataAccess.csproj
