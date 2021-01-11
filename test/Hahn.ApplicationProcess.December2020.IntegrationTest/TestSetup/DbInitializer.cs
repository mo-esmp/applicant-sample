using Hahn.ApplicationProcess.December2020.Data;

namespace Hahn.ApplicationProcess.December2020.IntegrationTest.TestSetup
{
    internal static class DbInitializer
    {
        public static void InitializeDb(ApplicantDbContext context)
        {
            foreach (var applicant in InitialData.Applicants)
                context.Applicants.Add(applicant);

            context.SaveChanges();
        }
    }
}