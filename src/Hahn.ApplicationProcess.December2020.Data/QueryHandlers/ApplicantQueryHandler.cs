using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Data.QueryHandlers
{
    public class ApplicantQueryHandler :
        IRequestHandler<ApplicantGetQuery, ApplicantEntity>,
        IRequestHandler<ApplicantGetsQuery, IEnumerable<ApplicantEntity>>

    {
        private readonly ApplicantDbContext _context;

        public ApplicantQueryHandler(ApplicantDbContext context)
        {
            _context = context;
        }

        public Task<ApplicantEntity> Handle(ApplicantGetQuery request, CancellationToken cancellationToken)
        {
            return _context.Applicants.AsNoTracking().FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        }

        public async Task<IEnumerable<ApplicantEntity>> Handle(ApplicantGetsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Applicants.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}