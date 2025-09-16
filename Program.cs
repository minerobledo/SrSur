using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SrSur;
using SrSur.Data;
using System;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- Configurar Kestrel solo en HTTP interno ---
builder.WebHost.UseKestrel()
       .ConfigureKestrel((context, options) =>
       {
           // Railway hará proxy a HTTPS externo, no necesitamos cert local
           options.ListenAnyIP(5000);  // HTTP interno
       });

// --- Servicios ---
builder.Services.AddInfrastructure(builder.Configuration);
Console.WriteLine("Servicios de infraestructura agregados.");

builder.Services.AddControllers();

// Swagger (solo en Development)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
Console.WriteLine("Iniciando configuración de ForwardedHeadersOptions para proxies.");
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
       ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
    options.KnownNetworks.Clear(); // Lo agregás si usás un proxy externo
    options.KnownProxies.Clear();
});
Console.WriteLine("ForwardedHeadersOptions configuradas.");

Console.WriteLine("Iniciando configuración de políticas CORS.");

// --- CAMBIO CLAVE AQUÍ: VOLVER A LEER DESDE LA CONFIGURACIÓN ---
// Comentamos o eliminamos la línea de hardcoding
// var hardcodedAllowedOrigins = new string[] { /* ... */ };
// Y volvemos a la línea original para leer desde builder.Configuration
var allowedOrigins = new[]
{
    "https://sr-sur-agency.web.app",
    "186.122.104.210"

}
.Where(origin => !string.IsNullOrWhiteSpace(origin)) // Filtrar nulos o vacíos
.ToArray();
// Mantenemos este log para confirmar qué valores se están leyendo.
Console.WriteLine($"Orígenes permitidos leídos de la configuración (Cors:AllowedOrigins): [{string.Join(", ", allowedOrigins)}]");

builder.Services.AddCors(options =>
{
    Console.WriteLine("Agregando política CORS 'DefaultPolicy'.");
    options.AddPolicy("DefaultPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins) // <-- Usamos la variable 'allowedOrigins' que se lee de la configuración
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
        Console.WriteLine($"Política 'DefaultPolicy' configurada con: Orígenes: {string.Join(", ", allowedOrigins)}, AllowAnyHeader, AllowAnyMethod, AllowCredentials.");
    });
});

var app = builder.Build();

app.UseCors("DefaultPolicy");
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<SrsurDbContext>();
//    db.Database.Migrate();
//}

// --- Middleware ---
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("sawger agregado");
    app.UseSwagger();
    app.UseSwaggerUI();
}

// No necesitamos redirección HTTPS
// app.UseHttpsRedirection();  <-- lo comentamos o eliminamos

app.UseAuthentication();
app.UseAuthorization();



// Mapear controladores
app.MapControllers();

// --- Ejecutar la app ---
app.Run();
