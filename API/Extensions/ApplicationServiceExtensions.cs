﻿using Application.Activities.Commands;
using Application.Activities.Queries;
using Application.Core;
using Application.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastracture.Photos;
using Infrastracture.Security;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        // IConfiguration - konfiguracja, ustawienia aplikacji, json
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
            IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            // services.AddDbContext<DataContext>(options =>
            // {
            //     options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            // });

            services.AddDbContext<DataContext>(options =>
            {
                
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string connStr;

                // Depending on if in development or production, use either FlyIO
                // connection string, or development connection string from env var.
                if (env == "Development")
                {
                    // Use connection string from file.
                    connStr = config.GetConnectionString("DefaultConnection");
                }
                else
                {
                    // Use connection string provided at runtime by FlyIO.
                    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                    // Parse connection URL to connection string for Npgsql
                    connUrl = connUrl.Replace("postgres://", string.Empty);
                    var pgUserPass = connUrl.Split("@")[0];
                    var pgHostPortDb = connUrl.Split("@")[1];
                    var pgHostPort = pgHostPortDb.Split("/")[0];
                    var pgDb = pgHostPortDb.Split("/")[1];
                    var pgUser = pgUserPass.Split(":")[0];
                    var pgPass = pgUserPass.Split(":")[1];
                    var pgHost = pgHostPort.Split(":")[0];
                    var pgPort = pgHostPort.Split(":")[1];
                    var updatedHost = pgHost.Replace("flycast", "internal");

                    connStr = $"Server={updatedHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};";
                }

                // Whether the connection string came from the local development configuration file
                // or from the environment variable from FlyIO, use it to set up your DbContext.
                options.UseNpgsql(connStr);
            });


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithExposedHeaders("WWW-Authenticate", "Pagination")
                        .WithOrigins("http://localhost:3000", "https://localhost:3000");
                });
            });

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(GetAllActivitiesHandler).Assembly);
            });

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateActivityHandler>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.AddSignalR();



            return services;
        }
    }
}
