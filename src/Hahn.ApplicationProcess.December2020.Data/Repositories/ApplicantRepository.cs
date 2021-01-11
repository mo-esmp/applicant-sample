using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Data.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly ApplicantDbContext _context;

        public ApplicantRepository(ApplicantDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(ApplicantEntity applicant)
        {
            _context.Applicants.Add(applicant);

            return Task.CompletedTask;
        }

        public Task EditAsync(ApplicantEntity applicant)
        {
            _context.Entry(applicant).State = EntityState.Modified;

            _context.Entry(applicant).CurrentValues.SetValues(applicant);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(int id)
        {
            var applicant = new ApplicantEntity { Id = id };
            _context.Attach(applicant);
            _context.Entry(applicant).State = EntityState.Deleted;

            return Task.CompletedTask;
        }

        public Task RemoveAsync(ApplicantEntity applicant)
        {
            _context.Entry(applicant).State = EntityState.Deleted;

            return Task.CompletedTask;
        }

        public ValueTask<ApplicantEntity> GetByIdAsync(int id)
        {
            return _context.Applicants.FindAsync(id);
        }
    }
}