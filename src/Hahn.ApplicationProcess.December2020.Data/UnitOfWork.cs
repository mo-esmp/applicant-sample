using Hahn.ApplicationProcess.December2020.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicantDbContext _context;

        public UnitOfWork(ApplicantDbContext context)
        {
            _context = context;
        }

        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}