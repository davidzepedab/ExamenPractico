global using database;
using ExamenPractico.Middlewares;
using Microsoft.EntityFrameworkCore;
using NLog.Fluent;
using NLog;
using NLog.Web;

//Iniciar Logger
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

//Capturar excepciones de ejecucion inicial con try catch(opcional para el examen)
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //Constructor para uso de NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //Inyeccion de Dependencia DB
    builder.Services.AddDbContext<InventarioContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ExamenPracticoConnection"))
    );

    var app = builder.Build();

    //Creacion de la BD al ejecutar el proyecto
    using (var scope = app.Services.CreateScope())
    {
        var Context = scope.ServiceProvider.GetRequiredService<InventarioContext>();
        Context.Database.Migrate();
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    //Middleware para el manejo de errores del lado servidor y el logging
    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.Run();
}
catch(Exception ex)
{
    logger.Error(ex);
    throw (ex);
}
finally
{
    //Borrar y detener los temporizadores internos antes de finalizar la aplicacion
    NLog.LogManager.Shutdown();
}
