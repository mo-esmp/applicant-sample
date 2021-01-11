using System.Collections.Generic;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using MediatR;

namespace Hahn.ApplicationProcess.December2020.Domain.Queries
{
    public record ApplicantGetsQuery : IRequest<IEnumerable<ApplicantEntity>>;
}