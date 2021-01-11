using Hahn.ApplicationProcess.December2020.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hahn.ApplicationProcess.December2020.IntegrationTest.TestSetup
{
    public class TestingWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                try
                {
                    var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var scopedServices = scope.ServiceProvider;

                    var dbContext = scopedServices.GetRequiredService<ApplicantDbContext>();
                    dbContext.Database.EnsureCreated();

                    DbInitializer.InitializeDb(dbContext);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                }
            });
        }
    }
}