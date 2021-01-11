using Hahn.ApplicationProcess.December2020.Domain.Entities;
using MediatR;

namespace Hahn.ApplicationProcess.December2020.Domain.Queries
{
    public record ApplicantGetQuery(int Id) : IRequest<ApplicantEntity>;
}