using Microsoft.Extensions.Hosting;

namespace Hahn.ApplicationProcess.December2020.Web.Infrastructure.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            //using var scope = host.Services.CreateScope();
            //var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            //try
            //{
            //    using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicantDbContext>();
            //    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            //    if (configuration.GetValue<bool>("MigrationEnabled") == false)
            //        return host;

            //    var applied = dbContext.GetService<IHistoryRepository>()
            //        .GetAppliedMigrations()
            //        .Select(m => m.MigrationId);

            //    var total = dbContext.GetService<IMigrationsAssembly>()
            //        .Migrations.Select(m => m.Key);

            //    var hasPendingMigration = total.Except(applied).Any();
            //    if (hasPendingMigration)
            //        dbContext.Database.Migrate();
            //}
            //catch (Exception ex)
            //{
            //    logger.LogError(ex, ex.Message);
            //    Console.WriteLine(ex);
            //    throw;
            //}

            return host;
        }
    }
}