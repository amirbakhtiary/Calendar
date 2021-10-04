using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using AutoMapper;
using Calendar.AplicationService.Queries.EventItemAggregate;
using Calendar.Endpoints.WebAPI.Extentions.Mapper;
using Calendar.Endpoints.WebAPI.Models;
using Calendar.Endpoints.WebAPI.Validators;
using Calendar.Infrastructure.Data.Sql;
using Calendar.Infrastructure.Data.Sql.Repository;
using Calendar.Infrastructure.Data.Sql.UnitOfWork;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
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
            services.AddScoped<IEventItemRepository, EventItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IValidator<CreateEventItemModel>, CreateEventItemModelValidator>();
            services.AddTransient<IValidator<UpdateEventItemModel>, UpdateEventItemModelValidator>();

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
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });
            return services;
        }

        public static IServiceCollection AddCustomizedDataStore(this IServiceCollection services, IConfiguration configuration)
        {
            if (!configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services
                        .AddDbContext<CalendarContext>(options => options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"),
                            b =>
                            {
                                b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                                b.MigrationsHistoryTable($"__{nameof(CalendarContext)}");
                            }));
            }
            else
            {
                services
                    .AddDbContext<CalendarContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            }
            return services;
        }

        public static IServiceCollection ConfigSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Calendar.Endpoints.WebAPI", Version = "v1" });
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Calendar Api",
                    Description = "A simple API to create or update events",
                    Contact = new OpenApiContact
                    {
                        Name = "Amir",
                        Email = "am.bakhtiary@gmail.com",
                        Url = new Uri("https://github.com/amirbakhtiary/")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IServiceCollection ConfigAutoMapper(this IServiceCollection services)
        {
            services.AddTransient(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper());
            return services;
        }

        public static IServiceCollection ConfigMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.Load(typeof(EventItemDto).GetTypeInfo().Assembly.GetName().Name));
            return services;
        }


        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }

        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var calendarContext = serviceScope.ServiceProvider.GetService<CalendarContext>();
            calendarContext.Database.Migrate();
        }
    }
}