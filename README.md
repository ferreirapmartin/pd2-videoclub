# Programación Distribuida II - Videoclub

Para este trabajo, la API REST fue desarrollada con tecnología ASP.NET Core (lenguaje C#), base de datos SQL Server y se utilizó como ORM, Entity Framework. 

La aplicación se encuentra contenerizada. 

Se validó correctamente su funcionamiento tanto en Windows como en Linux. Si bien en Mac no se pudo probar, debería funcionar.

## Comenzando

Todos los comandos se ejecutarán, a menos que se indique lo contrario, en el directorio root del proyecto, es decir, donde se encuentra la solución (archivo .sln). Además, en caso de ejecutarse sobre Windows deben hacerse con PowerShell, en Linux o Mac en un Terminal.

La aplicación utiliza Code First de Entity Framework, entonces, en caso de que no exista la base de datos, se creará.

### Pre-requisitos

Para poder desplegar la aplicación, es necesario tener instalado Docker y Docker Compose

### Verificar/Instalar Docker

Para verificar si se tiene instalado Docker, hay que ejecutar el comando:

```sh
docker --version
```

Dicho comando, debería retornar algo parecido a:

```
Docker version 19.03.12, build 48a66213fe
```

En caso de no tenerlo instalado, se debe descargar e instalar desde [aquí](https://docs.docker.com/engine/install/) siguiendo las instrucciones según el SO operativo utilizado.


### Verificar/Instalar Docker Compose

Para verificar si se tiene instalado Docker Compose, hay que ejecutar el comando:

```sh
docker-compose --version
```
Este comando, debería retornar algo parecido a:

```
docker-compose version 1.27.2, build 18f557f9
```

En caso de no tenerlo instalado, se debe descargar e instalar desde [aquí](https://docs.docker.com/compose/install) siguiendo las instrucciones según el SO operativo utilizado.

## Ejecutando las pruebas

Para ejecutar las pruebas unitarias es necesario correr:

En Windows
```
.\run-tests-in-docker.ps1
```

En Linux o Mac
```
./run-tests-in-docker.sh
```

> En Linux o Mac es posible que se necesiten agregar, por única vez, permisos de ejecución al usuario. Para ello, se debe correr:
> ```sh
> chmod +x run-tests-in-docker.sh
> ```


Una vez ejecutado el script run-tests-in-docker, se debería visualizar el resultado del test. Por ejemplo:

```
...
A total of 1 test files matched the specified pattern.
Html test results file : /src/TestResults/TestResult__4a247e9f9950_20201003_031112.html

Test Run Successful.
Total tests: 23
     Passed: 23
 Total time: 2.8302 Seconds
Removing intermediate container 4a247e9f9950
 ---> 140a51770697
Successfully built 140a51770697
Successfully tagged pd2-videoclub:test
...
```

Además, se debería crear un archivo en la carpeta `testresult` del directorio root del proyecto (si no existe la carpeta, también se debería crear) con un archivo HTML con el detalle de la ejecución. Según la salida de ejemplo previa, debería haberse creado `testresult/TestResult__4a247e9f9950_20201003_031112.html`

> Este archivo se copia desde el container al Host.

## Creando imagen y contenedores

Para crear la imagen, se debe ejecutar:

```sh
docker-compose build
```

Se debería ver algo como lo siguiente:

```
db uses an image...
...
Step 1/17 : FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
 ---> c4155a9104a8
Step 2/17 : WORKDIR /src
 ---> Using cache
 ---> b2306ce8a0ac
Step 3/17 : COPY ["src/ApiRest/ApiRest.csproj", "src/ApiRest/"]
 ---> Using cache
 ---> 485020133c34
Step 4/17 : RUN dotnet restore "src/ApiRest/ApiRest.csproj"
 ---> Using cache
 ---> 01def73b2b9a
Step 5/17 : COPY . .
 ---> 4dc215a1c7bc
Step 6/17 : WORKDIR "/src/src/ApiRest"
 ---> Running in 329e460600af
Removing intermediate container 329e460600af
 ---> 88753f289445
Step 7/17 : RUN dotnet build "ApiRest.csproj" -c Release -o /app/build
 ---> Running in 7e710e9ff85d
Microsoft (R) Build Engine version 16.7.0+7fb82e5b2 for .NET
Copyright (C) Microsoft Corporation. All rights reserved
...
```

Una vez creada la imagen de la API, para crear los contenedores (de la base de datos y de la API), es necesario ejecutar:

```sh
docker-compose up
```

Una vez hecho esto, se debería ver algo como

```
...
Creating network "pd2-videoclub_default" with the default driver
Creating pd2-videoclub-db       ... done
Creating pd2-videoclub-api      ... done
...
2020-10-03 03:49:36.17 spid22s     The Database Mirroring endpoint is in disabled or stopped state.
2020-10-03 03:49:36.20 spid22s     Service Broker manager has started.
2020-10-03 03:49:36.31 spid6s      Recovery is complete. This is an informational message only...
2020-10-03 03:49:36.36 spid24s     The default language (LCID 0) has been set for engine and f...
pd2-videoclub-api | info: ApiRest.Startup[0]
pd2-videoclub-api |       Se puede inicializar la base de datos correctamente
pd2-videoclub-api | info: Microsoft.Hosting.Lifetime[0]
pd2-videoclub-api |       Now listening on: http://[::]:80
pd2-videoclub-api | info: Microsoft.Hosting.Lifetime[0]
pd2-videoclub-api |       Application started. Press Ctrl+C to shut down.
pd2-videoclub-api | info: Microsoft.Hosting.Lifetime[0]
pd2-videoclub-api |       Hosting environment: Development
pd2-videoclub-api | info: Microsoft.Hosting.Lifetime[0]
pd2-videoclub-api |       Content root path: /app
```

Con esto, la base de datos quedará expuesta en el puerto 32632 y la API en el 5000

## Iniciando o deteniendo los contenedores

Una vez que los contenedores están creados, se puede detener la ejecución de ambos, con:

```sh
docker-compose stop
```
Y volver a iniciarlas con

```sh
docker-compose start
```

## Ejemplos

Dentro de la carpeta [`postman`](postman) se encuetra una colección de [Postman](https://www.postman.com/downloads/) con ejemplos de llamadas a la API.