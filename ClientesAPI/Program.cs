using Application.Interfaces;
using Application.Services;
using Application.Validators;
using ClientesAPI.Middlewares;
using Domain.Interfaces;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<ClientCreateDtoValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<ClientUpdateDtoValidator>();
    });

// Configuración de la conexión a la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada.");
}
// Configuración de Health Checks
builder.Services.AddHealthChecks()
    .AddCheck("api", () => HealthCheckResult.Healthy("API está funcionando"), tags: new[] { "api" })
    .AddDbContextCheck<AppDbContext>(
        name: "database",
        failureStatus: HealthStatus.Degraded,
        tags: new[] { "db" });

builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(15);
    options.MaximumHistoryEntriesPerEndpoint(50);
    options.AddHealthCheckEndpoint("API", "http://localhost/health-api");
    options.AddHealthCheckEndpoint("Base de Datos", "http://localhost/health-db");
})
.AddInMemoryStorage();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));
}

builder.Logging.AddConsole();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Ejecutar migraciones automáticamente
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurrió un error al ejecutar las migraciones.");
    }
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Clientes V1");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapHealthChecks("/health-api", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("api"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecks("/health-db", new HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("db"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
    options.ApiPath = "/health-ui-api";
});
app.MapControllers();
app.Run();

public partial class Program { }