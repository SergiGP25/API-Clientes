# API de Clientes

API REST para la gestión de clientes desarrollada con .NET 8, Entity Framework Core y SQL Server.

## Requisitos Previos

- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Estructura del Proyecto

```
API-Clientes/
├── ClientesAPI/          # Proyecto principal de la API
├── Application/          # Capa de aplicación (servicios, DTOs)
├── Domain/              # Capa de dominio (entidades, interfaces)
└── Infrastructure/      # Capa de infraestructura (repositorios, DbContext)
```

## CI/CD Pipeline

El proyecto incluye un pipeline de CI/CD configurado con GitHub Actions que:

1. Se ejecuta en cada push a main y en pull requests
2. Realiza las siguientes acciones:
   - Restaura las dependencias
   - Compila el proyecto
   - Ejecuta las pruebas
   - Construye la imagen Docker
   - Publica la imagen en Docker Hub

### Configuración del Pipeline

Para que el pipeline funcione, necesitas configurar los siguientes secrets en tu repositorio de GitHub:

- `DOCKERHUB_USERNAME`: Tu nombre de usuario en Docker Hub
- `DOCKERHUB_TOKEN`: Tu token de acceso de Docker Hub

### Verificar el Pipeline

1. Ve a la pestaña "Actions" en tu repositorio de GitHub
2. Selecciona el workflow "dotnet.yml"
3. Verifica que las ejecuciones sean exitosas

## Configuración de Docker

El proyecto incluye dos archivos principales para Docker:

- `Dockerfile`: Define cómo construir la imagen de la API
- `docker-compose.yml`: Define los servicios (API y base de datos)

## Instrucciones de Despliegue

### 1. Clonar el Repositorio

```bash
git clone <url-del-repositorio>
cd API-Clientes
```

### 2. Construir y Ejecutar con Docker Compose

```bash
# Construir y ejecutar los contenedores
docker-compose up --build

# Para ejecutar en segundo plano
docker-compose up -d --build
```

### 3. Verificar el Despliegue

- API: http://localhost/swagger
- Base de datos: localhost,1433
  - Usuario: sa
  - Contraseña: YourStrong!Passw0rd

### 4. Comandos Útiles

```bash
# Ver logs de los contenedores
docker-compose logs

# Ver logs en tiempo real
docker-compose logs -f

# Ver logs de un servicio específico
docker-compose logs api
docker-compose logs db

# Detener los contenedores
docker-compose down

# Detener y eliminar volúmenes
docker-compose down -v

# Reiniciar un servicio
docker-compose restart api

# Ver estado de los servicios
docker-compose ps
```

### 5. Migraciones

Las migraciones de la base de datos se ejecutan automáticamente al iniciar la aplicación. No es necesario ejecutar comandos manualmente.

## Variables de Entorno

El archivo `docker-compose.yml` incluye las siguientes variables de entorno:

### API
- `ASPNETCORE_ENVIRONMENT`: Development
- `ConnectionStrings__DefaultConnection`: Cadena de conexión a SQL Server

### Base de Datos
- `ACCEPT_EULA`: Y
- `SA_PASSWORD`: YourStrong!Passw0rd

## Volúmenes

- `sqldata`: Persiste los datos de SQL Server

## Puertos

- API: 80
- SQL Server: 1433

## Solución de Problemas

1. **La API no puede conectarse a la base de datos**
   - Verificar que el contenedor de la base de datos esté corriendo
   - Comprobar la cadena de conexión en docker-compose.yml
   - Revisar los logs: `docker-compose logs api`

2. **Error al construir la imagen**
   - Limpiar imágenes y contenedores: `docker system prune -a`
   - Reconstruir: `docker-compose up --build`

3. **Problemas con las migraciones**
   - Verificar los logs de la API
   - Asegurarse de que la base de datos esté accesible

4. **Problemas con el Pipeline**
   - Verificar que los secrets estén configurados correctamente
   - Revisar los logs del pipeline en GitHub Actions
   - Asegurarse de que el token de Docker Hub tenga los permisos necesarios

## Notas Importantes

- La base de datos se crea automáticamente al iniciar la aplicación
- Los datos persisten entre reinicios gracias al volumen `sqldata`
- Las migraciones se ejecutan automáticamente al iniciar la API
- La API está configurada para ejecutarse en modo desarrollo
- El pipeline de CI/CD se ejecuta automáticamente en cada push a main
