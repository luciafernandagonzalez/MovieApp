# 🎬 MovieApp API Backend - .NET Core Assessment

Este proyecto implementa un backend en .NET Core siguiendo principios de Clean Architecture para gestionar películas y sincronizar datos de la API pública de Star Wars.

## 🛠️ Requisitos Previos

Tener instalado:
1. **.NET 8 SDK** (o la versión que hayas usado).
2. **SQL Server LocalDB** (instalado con Visual Studio).
3. **dotnet-ef CLI Tool**: `dotnet tool install --global dotnet-ef`

## ⚙️ Configuración Inicial y Ejecución

Sigue estos pasos para levantar la aplicación y la base de datos por primera vez:

### Paso 1: Clonar el Repositorio

git clone https://github.com/luciafernandagonzalez/MovieApp.git

### Paso 2: Configuracion de base de datos

1. Revisa la Cadena de Conexión: Verifica que la DefaultConnection en MovieApp.API/appsettings.json apunte a un servidor LocalDB accesible.

2. Aplicar Migraciones: Ejecuta el siguiente comando para crear la base de datos y sembrar el primer usuario (Admin):
dotnet ef database update --startup-project MovieApp.API --project MovieApp.Infrastructure

### Paso 3: Iniciar la app

Ejecutar 
dotnet run --project MovieApp.API

## DOCUMENTACION SWAGGER API

https://localhost:7167/swagger/index.html
