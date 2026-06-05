#!/usr/bin/env bash

dotnet tool restore

docker compose up -d

dotnet restore

dotnet run --project ClinicManager