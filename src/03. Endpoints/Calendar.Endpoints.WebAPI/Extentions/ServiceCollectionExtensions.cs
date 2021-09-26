using System.Reflection;
using System.Text.Json;
using AutoMapper;
using Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem;
using Calendar.Core.Domain.Commons;
using Calendar.Infrastructure.Data.Sql;
using Calendar.Infrastructure.Data.Sql.Commons;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

namespace Calendar.Endpoints.WebAPI.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomizeControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
            return services;
        }

        public static IServiceCollection AddCustomizeService(this IServiceCollection services)
        {
            services.AddScoped<IRepository, CalendarRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection ConfigApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {

                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Title = "One or more validation errors have occurred."
                        //Detail = "Please refer to the errors property for additional details.",
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                    //return new BadRequestObjectResult(new ErrorDetails(problemDetails.Status, problemDetails.Title, problemDetails.Instance,
                    //    context.ModelState.Values.SelectMany(x => x.Errors)
                    //        .Select(x => new Error(x.ErrorMessage))
                    //    ))
                    //{
                    //    ContentTypes = { "application/problem+json", "application/problem+xml" }
                    //};
                };
            });
            return services;
        }

        public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<CalendarDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"),
                    b =>
                    {
                        b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                        b.MigrationsHistoryTable($"__{nameof(CalendarDBContext)}");
                    }));

            return services;
        }

        public static IServiceCollection ConfigSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Calendar.Endpoints.WebAPI", Version = "v1" });
            });

            return services;
        }

        public static IServiceCollection ConfigAutoMapper(this IServiceCollection services)
        {
            services.AddTransient(provider => new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile(new DataProfile());
                //cfg.AddProfile(new Mapper.DataProfile(provider.GetService<IHttpContextAccessor>()));
            }).CreateMapper());
            return services;
            //services.AddAutoMapper(Assembly.Load(typeof(AppIdentityDbContext).GetTypeInfo().Assembly.GetName().Name));
        }

        public static IServiceCollection ConfigMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.Load(typeof(AddNewEventItemCommand).GetTypeInfo().Assembly.GetName().Name),
                Assembly.Load(typeof(CalendarDBContext).GetTypeInfo().Assembly.GetName().Name));
            return services;
        }


        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }
    }
}