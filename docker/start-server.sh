#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Staging

dotnet /app/Taxonomy.Api.dll & nginx -g 'daemon off;'
