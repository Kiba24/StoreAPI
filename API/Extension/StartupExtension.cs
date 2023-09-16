using AspNetCoreRateLimit;
using Core.Interfaces;
using Core.Mappings;
using Infrastructure.Data;
using Infrastructure.Unit_Of_Work;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text;
using API.Helpers;
using API.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extension
{
    public static class StartupExtension
    {
        public static void Initialize(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDatabase(services, configuration);
            ConfigureCors(services);
            ConfigureJWT(services,configuration);

            //Automapper
            services.AddAutoMapper(Assembly.GetEntryAssembly());
            services.AddAutoMapper(typeof(ProductProfile)); // Register an AutoMapper profile


            //Scoping unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IUserService, UserService>();
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

        //Limit quantity of request recieved from the same IP in a period of time
        public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IRateLimitConfiguration,RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();

            services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.HttpStatusCode = 429; //Too many request
                options.StackBlockedRequests = false;
                options.RealIpHeader = "X-Real-Ip";

                options.GeneralRules = new List<RateLimitRule>
                {
                    new RateLimitRule()
                    {
                        Endpoint ="*",
                        Period = "10s",
                        Limit = 10
                    }
                };
            });
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JWT>(config.GetSection("JWT"));

            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["JWT:Issuer"],
                        ValidAudience = config["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]))
                    };
                });
        }
    }
}
