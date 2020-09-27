#!/usr/bin/env bash

dotnet ef database update -s ./TasteRazor/TasteRazor.csproj -p ./TasteRazor.DataAccess/TasteRazor.DataAccess.csproj
