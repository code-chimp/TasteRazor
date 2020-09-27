#!/usr/bin/env pwsh
param(
    [string]$migration = $(throw "-migration is required")
)

dotnet ef migrations add $migration -s .\TasteRazor\TasteRazor.csproj -p .\TasteRazor.DataAccess\TasteRazor.DataAccess.csproj
