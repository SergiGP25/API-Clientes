#!/bin/bash

# Esperar a que la base de datos esté lista
echo "Esperando a que la base de datos esté lista..."
sleep 10

# Ejecutar las migraciones
echo "Ejecutando migraciones..."
dotnet ef database update

# Iniciar la aplicación
echo "Iniciando la aplicación..."
dotnet ClientesAPI.dll 