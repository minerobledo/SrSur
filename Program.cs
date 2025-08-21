using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SrSur;
using System.Text;



var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

// Configurar Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    // Si querés forzar HTTP y HTTPS
    options.ListenAnyIP(5000); // HTTP
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS con certificado de dev
    });
});

builder.Services.AddInfrastructure(builder.Configuration);
Console.WriteLine("Servicios de infraestructura agregados.");



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
