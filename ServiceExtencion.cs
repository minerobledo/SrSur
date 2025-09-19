using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SrSur.Data;
using SrSur.Interface;
using SrSur.Models.DTOs.Imput.Sponsor;
using SrSur.Models.DTOs.output;
using SrSur.Models.Entities;
using SrSur.Repositories;
using SrSur.Services;
using System;
using System.Security.AccessControl;
using System.Text;

namespace SrSur
{
    public static  class ServiceExtencion
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<SrsurDbContext>(options =>
               options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")));

            // Configurar JWT
            var jwt = configuration.GetSection("Jwt");


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = ctx =>
                    {
                        Console.WriteLine($"JWT Error: {ctx.Exception.Message}");
                        return Task.CompletedTask;
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt["Issuer"],
                    ValidAudience = jwt["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!))
                };
            });

            AutomaterConfig(services);
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostsVisitedByGuestRepository, PostVisitedByGusetRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserRepository,UserRepository>();

            services.AddScoped<CategoryService>();
            services.AddScoped<CommentServices>();
            services.AddScoped<GuestServices>();
            services.AddScoped<JWTService>();
            services.AddScoped<PostServices>();
            services.AddScoped<PostsVisitedByGuestServise>();
            services.AddScoped<TagSeervice>();
            services.AddScoped<UserService>();
        }
        private static void AutomaterConfig(IServiceCollection services)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Category,CategoryOutnDto>();
                mc.CreateMap<Tag, TagOutDto>();
                mc.CreateMap<Sponsor, CreateSponsorDto>();

            }, loggerFactory);

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }

}
