using FluentValidation;
using Hahn.ApplicationProcess.December2020.Data;
using Hahn.ApplicationProcess.December2020.Data.QueryHandlers;
using Hahn.ApplicationProcess.December2020.Data.Repositories;
using Hahn.ApplicationProcess.December2020.Domain;
using Hahn.ApplicationProcess.December2020.Domain.CommandHandlers;
using Hahn.ApplicationProcess.December2020.Domain.Commands;
using Hahn.ApplicationProcess.December2020.Domain.Repositories;
using Hahn.ApplicationProcess.December2020.Web.Infrastructure.Extensions;
using Hahn.ApplicationProcess.December2020.Web.Infrastructure.HttpClients;
using Hahn.ApplicationProcess.December2020.Web.Infrastructure.Swagger;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Polly;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace Hahn.ApplicationProcess.December2020.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options => options.AddPolicy("AllowedOrigins", builder =>
            {
                var origins = Configuration.GetSection("AllowedOrigins").Get<string[]>();
                builder.WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials();
            }));
            services.AddDbContext<ApplicantDbContext>(options => options.UseInMemoryDatabase("ApplicantDb"));

            services.AddValidatorsFromAssemblyContaining<ApplicantAddCommand>();
            services.AddMediatR(typeof(ApplicantAddCommand), typeof(ApplicantQueryHandler));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddHttpClient<ICountryRepository, CountryHttpClient>(options =>
                options.BaseAddress = new Uri(Configuration["RestCountries:BaseUrl"]))
                .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));

            AddSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowedOrigins");
            app.UseApiExceptionHandling();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddApiVersioning();
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.ExampleFilters();
            });
        }
    }
}