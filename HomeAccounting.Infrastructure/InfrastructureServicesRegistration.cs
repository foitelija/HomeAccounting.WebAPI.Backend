using HomeAccounting.Application.Interfaces.Identity;
using HomeAccounting.Application.Interfaces.Infrastructure;
using HomeAccounting.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using HomeAccounting.Application.Models.Identity;
using Quartz;
using Microsoft.Extensions.Logging;

namespace HomeAccounting.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = JobKey.Create(nameof(LoggingBackgroundJob));
                options.AddJob<LoggingBackgroundJob>(jobKey)
                .AddTrigger(trigger => trigger.ForJob(jobKey).WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(5).RepeatForever()));
            });
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.AddLogging(config =>
            {
                config.AddDebug();
                config.AddConsole();
            });

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IPurchaseReportService, PurchaseReportService>();
            services.AddHttpClient<CurrencyService>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => 
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false, // Для Refresh-token надо тут поставить на false будет.
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
            });

            return services;
        }
    }
}
