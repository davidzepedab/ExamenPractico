# ExamenPractico
<table>
<tr>
<td>
WebAPI para manejar datos de un inventario con articulos, almacenes y stock. Utilizando Entity Framework pensado con Code First y NLog para el logging. SQLServer para
la base datos.
</td>
</tr>
</table>

## Herramientas
- [.Net Core 7.0.102](https://dotnet.microsoft.com/en-us/) - Uso de .Net Core brinda una flexibilidad y compatibilidad mayor con otras plataformas a comparacion del .Net Framework.
- [Visual Studio 2022](https://visualstudio.microsoft.com/es/) - Poderoso IDE para desarrollar en C# con completa compatibilidad para .Net Core.
- [NLog Web 5.2.2](https://www.nuget.org/packages/NLog.Web.AspNetCore) - Util extension que integra las librerias Logging por defecto para brindar opciones ricas en loggeo de output con detalles de HttpContext.
- [SQL Server 2022](https://www.microsoft.com/es-es/sql-server/sql-server-2022) - Popular sistema de base de datos para el entorno Microsoft, utilizado junto a SSMS 19.0.1.

## Observaciones e Instalación Rapida
La solucion se ejecuta desde visual studio. Utiliza los paquetes NuGet [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/7.0.3?_src=template), [Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/7.0.3?_src=template), [NLog.Extensions.Logging](https://www.nuget.org/packages/NLog.Extensions.Logging/5.2.2?_src=template), [NLog.Web.AspNetCore](https://www.nuget.org/packages/NLog.Web.AspNetCore/5.2.2?_src=template) y Swashbuckle para Swagger (por defecto)

La cadena de conexion para la base de datos se encuenta en el archivo `appsettings.json` bajo el nombre de `ExamenPracticoConnection`. La opcion de `TrustServerCertificate` es opcional.

En el archivo `Program.cs` se incluyó un codigo para crear la base de datos automaticamente en caso de no existir.

Para establecer la direccion de la generacion del archivo de logs acceder al archivo `NLog.config`, en la seccion de targets en el unico target presente. 
La propiedad fileName establece el path para le generacion del archivo. 

El path viene dinamico por defecto utilizando la variable `${basedir}` para generar una carpeta en la direccion base de la ejecucion llamada logs y guardar los .txt ahi.

La direccion es: **ExamenPractico\bin\Debug\net6.0\logs**

El programa utiliza **Swagger** para utilizar el servicio y testear las requests y las responses de la API.

Por el diseño del examen se asume que la tabla **Almacens** ya viene poblada por defecto. Por lo que para razones de desarrollo y debugging se utilizo el siguiente query:

```sql
use inventario_db

insert into Almacens values('Almacen Palo Verde','Almacen de 2 pisos, en la colonia palo verde con cp 83280')
insert into Almacens values('Almacen San Benito','Almacen rentado, en la colonia San Benito con cp 83190')
insert into Almacens values('Almacen Balderrama','Almacen bodega aurrera, en la colonia balderrama')
insert into Almacens values('Almacen Pitic','Almacen en el centro de hermosillo')
```

---

## Diseño del funcionamiento

### Entity Framework
Como se pidio en un principio, el sistema utiliza Entity Framework para crear los modelos de la base de datos. Utilizando el approach Code First para definir antes que nada a travez de codigo las entidades que manejara nuestra base de datos. Los archivos relacionados a la base de datos se implementaron dentro de una biblioteca de clases llamada `database` para mejor orden. 

En dicha biblioteca podemos encontrar la clase **Almacen**, **Articulo**, y **Stock**. Estas con sus debidos atributos. Los modelos no tienen relacion entre si (Foreign Key). Tambien podemos encontrar el archivo `InventarioContext` que nos proporciona el contexto para la base de datos. Utilizando la clase DbContext e incluye el constructor para las entidades.

Los archivos relacionados a migraciones se encuentran bajo `Migrations`, actualmente cuenta con una migracion.

### Controladores

Dentro del proyecto **ExamenPractico** encontramos la carpeta Controllers que incluye los metodos para las llamadas Http de la API.

Cabe mencionar que los metodos estan hechos asincronos.

### Control de excepciones

Para el manejo de errores de lado del servidor utilizamos un middleware que se encarga de procesar cada request y de devolver un codigo 500 en caso de existir un error interno en el servidor. Este se encuentra en la carpeta `Middlewares` bajo el nombre `ErrorHandlingMiddleware`

Los errores de lado de cliente son manejados dentro de los controladores utilizando la clase `ActionResult` y retornará a grandes rasgos un `BadRequest` con errores 400 mencionando que el articulo no existe. 

### Loggeo

Para el Loggeo de errores se utilizó el paquete NuGet **NLog** ya que brinda funcionalidades superiores de logging asi como mayor flexibilidad. El archivo `NLog.config` contiene la configuracion del loggeador. El sistema actualmente esta configurado para registrar los **Errores** y los errores **Criticos** con la fecha, el tipo y la excepcion.

### Program.cs

En el archivo del programa se encuentra la inicializacion de las funciones personalizadas o librerias utilizadas. En el cual podemos encontrar el montaje del Loggeador, la llamada al Middleware y las inyeccions de dependcias para la base de datos.

De igual manera, encontraremos un codigo que intentara crear la base de datos al ejecutar el sistema en caso de no encontrarla.



