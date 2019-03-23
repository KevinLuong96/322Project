#!/bin/bash
export migration=migration_$(find ./Migrations -type f | wc -l | xargs) 

echo ""ADDING_MIGRATION
dotnet ef migrations add $migration
echo APPLYING_MIGRATION
dotnet ef migrations script 0 $migration -i -o migrate.sql
docker-compose down
docker-compose build
docker-compose up

