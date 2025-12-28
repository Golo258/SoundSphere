#!/bin/bash

dotnet ef migrations add InitialMigration

if [ $? -ne 0 ]; then
	echo "An error occurs during creation of migrations"
	exit 1 
fi

dotnet ef database update InitialMigration

if [ $? -ne 0 ]; then
        echo "An error occurs during database update"
        exit 1
fi

dotnet run seeddata

if [ $? -ne 0 ]; then
        echo "An error occurs during data initialization"
        exit 1
fi

dotnet watch run 

