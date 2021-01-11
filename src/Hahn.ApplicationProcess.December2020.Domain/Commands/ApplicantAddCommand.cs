using FluentValidation;
using Hahn.ApplicationProcess.December2020.Domain.Repositories;
using MediatR;

namespace Hahn.ApplicationProcess.December2020.Domain.Commands
{
    public record ApplicantAddCommand(
        string Name,
        string FamilyName,
        int Age,
        string EmailAddress,
        string Address,
        string CountryOfOrigin,
        bool Hired
        ) : IRequest<int>;

    public class ApplicantAddCommandValidator : AbstractValidator<ApplicantAddCommand>
    {
        public ApplicantAddCommandValidator(ICountryRepository repository)
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50);

            RuleFor(v => v.FamilyName)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50);

            RuleFor(v => v.Address)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(256);

            RuleFor(v => v.EmailAddress)
                .NotEmpty()
                .EmailAddress();

            RuleFor(v => v.Age)
                .NotEmpty()
                .GreaterThan(19)
                .LessThan(61);

            RuleFor(v => v.CountryOfOrigin)
                .ValidCountryName(repository);
        }
    }
}