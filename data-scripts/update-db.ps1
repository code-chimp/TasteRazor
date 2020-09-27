#!/usr/bin/env pwsh

dotnet ef database update -s .\TasteRazor\TasteRazor.csproj -p .\TasteRazor.DataAccess\TasteRazor.DataAccess.csproj
