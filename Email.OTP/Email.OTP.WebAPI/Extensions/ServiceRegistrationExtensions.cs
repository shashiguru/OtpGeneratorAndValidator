using Email.OTP.Application.Interfaces;
using Email.OTP.Application.Models.AppSettings;
using Email.OTP.Application.Services;
using Email.OTP.Infrastructure;
using Email.OTP.Infrastructure.Email;
using Email.OTP.Infrastructure.Interfaces;
using Email.OTP.Infrastructure.Repository;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Email.OTP.WebAPI.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        /// <summary>
        /// ConfigureController with fluent validators.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureController(this IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(options =>
            {
                // Validate child properties and root collection elements
                options.ImplicitlyValidateChildProperties = true;
                options.ImplicitlyValidateRootCollectionElements = true;

                // Automatic registration of validators in assembly
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
            return services;
        }

        /// <summary>
        /// ConfigureServicesRegistrations
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureServicesRegistrations(this IServiceCollection services)
        {
            services.AddScoped<IUserDetailRepository, UserDetailRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailOpertions, EmailOperations>();
            return services;
        }

        /// <summary>
        /// ConfigureDatabase
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OtpGenerateDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(OtpGenerateDbContext).Assembly.FullName)
                   .EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null)));
            services.AddScoped<IOtpGenerateDbContext>(provider => provider.GetService<OtpGenerateDbContext>());
            return services;
        }

        /// <summary>
        /// ConfigureCors
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            return services;
        }

        /// <summary>
        /// ConfigureOptions
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(services.Configure<EmailConfig>(configuration.GetSection(nameof(EmailConfig))));
            return services;
        }

        /// <summary>
        /// ConfigureSwagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Emai OTP Validator API",
                    Version = "v1",
                    Description = "API to generate Otp and validate"
                });
                c.EnableAnnotations();
                c.TagActionsBy(api => api.HttpMethod);
                c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            });
            return services;
        }
    }
}
        
