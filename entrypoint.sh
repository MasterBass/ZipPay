#!/bin/bash

set -e
run_cmd="dotnet ./TestProject.API/bin/Release/netcoreapp3.1/TestProject.API.dll"

until dotnet ef database update --project ./TestProject.Storage.Migrations -s ./TestProject.API; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"
exec $run_cmd