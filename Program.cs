using Microsoft.AspNetCore.Authentication.JwtBearer;

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

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SrsurDbContext>();
    db.Database.Migrate();
}

// --- Middleware ---
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("sawger agregado");
    app.UseSwagger();
    app.UseSwaggerUI();
}

// No necesitamos redirección HTTPS
// app.UseHttpsRedirection();  <-- lo comentamos o eliminamos

app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// --- Ejecutar la app ---
app.Run();
