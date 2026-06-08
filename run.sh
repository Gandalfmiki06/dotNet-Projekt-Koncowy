#!/usr/bin/env bash

dotnet tool restore

docker compose up -d

dotnet restore

dotnet ef database update --project ClinicManager.Web

dotnet run --project ClinicManager.Web