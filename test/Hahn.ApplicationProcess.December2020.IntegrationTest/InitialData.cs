using Hahn.ApplicationProcess.December2020.Domain.Entities;
using System.Collections.Generic;

namespace Hahn.ApplicationProcess.December2020.IntegrationTest
{
    internal class InitialData
    {
        private static readonly List<ApplicantEntity> _applicants = new()
        {
            new ApplicantEntity
            {
                Id = 20,
                Address = "This is an address",
                Age = 21,
                CountryOfOrigin = "Germany",
                EmailAddress = "applicant20@dmain.com",
                FamilyName = "Lukas",
                Name = "Elias"
            }
        };

        public static IEnumerable<ApplicantEntity> Applicants => _applicants;
    }
}