#!/bin/bash
export migration=migration_$(find ./Migrations -type f | wc -l | xargs) 
dotnet ef migrations add $migration
dotnet ef migrations script 0 $migration -i -o migrate.sql
docker-compose down
docker-compose build
docker-compose up

