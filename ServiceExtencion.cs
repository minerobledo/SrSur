using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Repositories;
using SrSur.Services;
using System;
using System.Text;

namespace SrSur
{
    public static  class ServiceExtencion
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<SrsurDbContext>(options =>
               options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Configurar JWT
            var jwt = configuration.GetSection("Jwt");


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt["Issuer"],
                    ValidAudience = jwt["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!))
                };
            });
            services.AddScoped <IUserRepository,UserRepository>();
            services.AddScoped<UserService>();
            services.AddScoped<JWTService>();
        }
    }
}
