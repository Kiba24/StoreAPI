﻿using Core.Interfaces;
using Core.Mappings;
using Infrastructure.Data;
using Infrastructure.Unit_Of_Work;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace API.Extension
{
    public static class StartupExtension
    {
        public static void Initialize(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDatabase(services, configuration);
            ConfigureCors(services);

            //Automapper
            services.AddAutoMapper(Assembly.GetEntryAssembly());
            services.AddAutoMapper(typeof(ProductProfile)); // Register an AutoMapper profile


            //Scoping unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }

        private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
            });

            services.AddScoped<DataSeed>();

        }

        private static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });
        }

        public async static void ConfigureSeed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<StoreContext>();
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                // Call the data seeding method
                await DataSeed.SeedAsyncTask(context, loggerFactory);
            }
        }
    }
}
