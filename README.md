# PD II - Videoclub

Para este trabajo, la API REST fue desarrollada con tecnología ASP.NET Core (lenguaje C#), base de datos SQL Server Express LocalDB y se utilizó como ORM, Entity Framework.

Como se mencionará luego, la aplicación necesita ejecutarse sobre Windows, esta limitación existe porque utilizo la base da datos SQL Server Express LocalDB, la cual solo funciona en Windows. Consideré que es mejor esta limitación a tener que instalar una base de datos de mayor peso o apuntar a una remota. 

## Comenzando

Todos los comandos que se ejecutarán, a menos que se indique lo contrario, se pueden correr tanto con un CMD como con PowerShell. Además, todos se ejecutarán estando en el directorio root del proyecto, es decir, donde se encuentra la solución (archivo .sln).

La aplicación utilizá Code First de Entity Framework, entonces, en caso de que no exista la base de datos, la creará. 

### Pre-requisitos

Para que la aplicación corra, debe ejecutarse en un sistema operativo Windows 10 (en Windows 7 y 8 debería funcionar, pero no se verificó)

### Verificar/Instalar base de datos

La base datos que se utilizó es MSSQLLocalDB, para verificar si se tiene instalado, hay que ejecutar el comando “sqllocaldb info”

```bat
sqllocaldb info
```

Dicho comando, debería retornar


```
MSSQLLocalDB
```

En caso de no tenerlo instalado, se debe descargar (54MB) e instalar desde el [Centro de Descargas Microsoft](https://download.microsoft.com/download/7/c/1/7c14e92e-bdcb-4f89-b7cf-93543e7112d1/SqlLocalDB.msi)


### Verificar/Instalar SDK .NET Core 3.1

Para correr la aplicación (compilar, ejecutar y testear) es necesario tener instalado .NET Core 3.1 SDK. Para verificar esto, se debe correr el comando “dotnet --list-sdks”

```bat
dotnet --list-sdks
```
Se debería observar algo como lo siguiente:

```bat
3.1.100 [C:\Program Files\dotnet\sdk]
```

Si se tiene más de una versión instalada se vería una lista. Verificar que se tenga instalada la versión 3.1.X

En caso de no tenerlo instalado, se debe descargar (125MB) e instalar desde [Dotnet Microsoft](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.301-windows-x64-installer)


### Instalar dependencias Nuget

Para instalar las dependencias utilizadas en la solución, es necesario ejecutar

```cmd
dotnet restore
```

Se debería ver algo como:

```
  Restore completed in 41,02 ms for ...\src\Domain\Domain.csproj.
  Restore completed in 65,63 ms for ...\src\DataAccess\DataAccess.csproj.
  Restore completed in 65,63 ms for ...\src\ApiRest\ApiRest.csproj.
  Restore completed in 66,63 ms for ...\test\ApiRestTests\ApiRestTests.csproj.
```

## Ejecutando las pruebas

Para ejecutar las pruebas unitarias es necesario correr

```
dotnet test
```

Una vez hecho esto, se debería visualizar el resultado del test. Por ejemplo:

```
Test Run Successful.
Total tests: 24
     Passed: 24
 Total time: 9,5861 Seconds
```

## Publicando la aplicación

Para publicar la aplicación, es necesario ejecutar el siguiente comando (se creará una carpeta llamada `publish` en el directorio root con los binarios):

```bat
dotnet publish "src/ApiRest/ApiRest.csproj" -c Release -o ./publish
```

Se debería ver algo como lo siguiente:

```bat
Microsoft (R) Build Engine version 16.4.0+e901037fe for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Restore completed in 37,49 ms for ...\src\Domain\Domain.csproj.
  Restore completed in 70,67 ms for ...\src\ApiRest\ApiRest.csproj.
  Restore completed in 70,68 ms for ...\src\DataAccess\DataAccess.csproj.
  Domain -> ...\src\Domain\bin\Release\netcoreapp3.1\Domain.dll
  DataAccess -> ...\src\DataAccess\bin\Release\netcoreapp3.1\DataAccess.dll
  ApiRest -> ...\src\ApiRest\bin\Release\netcoreapp3.1\ApiRest.dll
  ApiRest -> ...\publish\
```

## Ejecutando la aplicación

Para correr la aplicación, es necesario ejecutar el siguiente comando:

```bat
dotnet run --project "src/ApiRest/ApiRest.csproj"
```

Se debería ver algo como lo siguiente (notar que se indica el puerto donde comenzó a correr):

```bat
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: ...\src\ApiRest
```

## Ejemplos

Dentro de la carpeta `postman` se encuetra una colección de [Postman](https://www.postman.com/downloads/) con ejemplos de llamadas a la API.