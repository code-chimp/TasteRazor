#!/usr/bin/env bash

# stand up a SQL Server instance
docker run --name TasteRazor_dev \
    -p 1418:1433 \
    -e 'ACCEPT_EULA=Y' \
    -e 'SA_PASSWORD=P@ssw0rd' \
    -d mcr.microsoft.com/mssql/server:2019-latest

# give the server time to start
sleep 5s

# run entity framework migrations
dotnet restore
dotnet ef database update -s ./TasteRazor/TasteRazor.csproj -p ./TasteRazor.DataAccess/TasteRazor.DataAccess.csproj
