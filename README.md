# ExamenPractico
<table>
<tr>
<td>
WebAPI para manejar datos de un inventario con articulos, almacenes y stock. Utilizando Entity Framework pensado con Code First y NLog para el logging. SQLServer para
la base datos.
</td>
</tr>
</table>

## Desarrollo
- [.Net Core 7.0.102](https://dotnet.microsoft.com/en-us/) - Uso de .Net Core brinda una flexibilidad y compatibilidad mayor con otras plataformas a comparacion del .Net Framework.
- [Visual Studio 2022](https://visualstudio.microsoft.com/es/) - Poderoso IDE para desarrollar en C# con completa compatibilidad para .Net Core.
- [NLog Web 5.2.2](https://www.nuget.org/packages/NLog.Web.AspNetCore) - Util extension que integra las librerias Logging por defecto para brindar opciones ricas en loggeo de output con detalles de HttpContext.
- [SQL Server 2022](https://www.microsoft.com/es-es/sql-server/sql-server-2022) - Popular sistema de base de datos para el entorno Microsoft, utilizado junto a SSMS 19.0.1.

## Observaciones y Ejecucion Rapida
La solucion se ejecuta desde visual studio. Utiliza los paquetes NuGet [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/7.0.3?_src=template), [Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/7.0.3?_src=template), [NLog.Extensions.Logging](https://www.nuget.org/packages/NLog.Extensions.Logging/5.2.2?_src=template), [NLog.Web.AspNetCore](https://www.nuget.org/packages/NLog.Web.AspNetCore/5.2.2?_src=template) y Swashbuckle para Swagger (por defecto)

La cadena de conexion para la base de datos se encuenta en el archivo *appsetting.json* bajo el nombre de ExamenPracticoConnection. La opcion de TrustServerCertificate es opcional.

Para establecer la direccion de la generacion del archivo de logs acceder al archivo *NLog.config*, en la seccion de targets en el unico target presente. 
La propiedad fileName establece el path para le generacion del archivo. 

El path viene dinamico por defecto utilizando las variables **${basedir}** para generar una carpeta en la direccion base de la ejecucion llamada logs y guardar los .txt ahi.

La direccion es: **ExamenPractico\bin\Debug\net6.0\logs**

