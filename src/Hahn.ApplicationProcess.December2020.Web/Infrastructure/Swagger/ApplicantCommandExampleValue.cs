using Hahn.ApplicationProcess.December2020.Domain.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace Hahn.ApplicationProcess.December2020.Web.Infrastructure.Swagger
{
    public class ApplicantCommandExampleValue : IExamplesProvider<ApplicantAddCommand>
    {
        public ApplicantAddCommand GetExamples()
        {
            return new ApplicantAddCommand("Mohsen", "Esmailpour", 35, "some@one.com", "This is an address", "Germany", true);
        }
    }
}