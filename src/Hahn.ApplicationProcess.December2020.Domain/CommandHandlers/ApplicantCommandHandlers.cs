using Hahn.ApplicationProcess.December2020.Domain.Commands;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Exceptions;
using Hahn.ApplicationProcess.December2020.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Domain.CommandHandlers
{
    public class ApplicantCommandHandlers :
        IRequestHandler<ApplicantAddCommand, int>,
        IRequestHandler<ApplicantEditCommand>,
        IRequestHandler<ApplicantRemoveCommand>
    {
        private readonly IApplicantRepository _repository;

        public ApplicantCommandHandlers(IApplicantRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(ApplicantAddCommand request, CancellationToken cancellationToken)
        {
            var applicant = new ApplicantEntity
            {
                Address = request.Address,
                Age = request.Age,
                CountryOfOrigin = request.CountryOfOrigin,
                EmailAddress = request.EmailAddress,
                FamilyName = request.FamilyName,
                Hired = request.Hired,
                Name = request.Name
            };
            await _repository.AddAsync(applicant);

            return applicant.Id;
        }

        public async Task<Unit> Handle(ApplicantEditCommand request, CancellationToken cancellationToken)
        {
            var applicant = await _repository.GetByIdAsync(request.ApplicantId);
            if (applicant == null)
                throw new NotFoundException("Applicant could not be found");

            applicant.Address = request.Address;
            applicant.Age = request.Age;
            applicant.CountryOfOrigin = request.CountryOfOrigin;
            applicant.EmailAddress = request.EmailAddress;
            applicant.FamilyName = request.FamilyName;
            applicant.Hired = request.Hired;
            applicant.Name = request.Name;

            await _repository.EditAsync(applicant);

            return Unit.Value;
        }

        public async Task<Unit> Handle(ApplicantRemoveCommand request, CancellationToken cancellationToken)
        {
            var applicant = await _repository.GetByIdAsync(request.ApplicantId);
            if (applicant == null)
                throw new NotFoundException("Applicant could not be found");

            await _repository.RemoveAsync(applicant);

            return Unit.Value;
        }
    }
}